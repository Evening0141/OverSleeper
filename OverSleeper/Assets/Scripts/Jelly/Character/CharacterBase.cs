using UnityEngine;
using System.Collections;

public class CharacterBase : MonoBehaviour
{
    // 変数まとめ
    #region field
    protected int hp;     　　 // プレイヤーHP
    protected string nameId;   // プレイヤー名
    protected float hit;  　　 // 射撃精度
    protected float moveSpd;   // 移動速度
    protected float resSpd;    // リスポーンクールタイム
    protected bool IsC = false; // チート使用   

    [Header("基本設定")]
    [SerializeField] protected Transform firePoint;         // 弾の発射位置
    [SerializeField] protected GameObject bulletPrefab;     // 弾のプレハブ
    [SerializeField] protected Camera npcCamera;            // NPCの視界カメラ
    [SerializeField] protected LayerMask wallLayer;         // 壁用レイヤー
    [SerializeField] protected LayerMask enemyLayer;        // 敵用レイヤー
    [SerializeField] protected Animator anim;               // アニメーター

    protected Vector3 moveDirection;                         // 移動方向
    protected float directionChangeInterval = 5f;            // 移動方向変更間隔
    protected float directionTimer = 0f;                     // 移動方向変更用タイマー
    protected float shotCooldown = 1f;                       // 弾のクールダウン
    protected float shotTimer = 0f;                          // 弾発射タイマー

    protected bool isMoving = true;                          // 移動中かどうか
    protected float moveStopTimer = 0f;                      // 停止・移動切替のためのタイマー
    protected float moveDuration = 0f;                       // 現在の「移動時間」
    protected float stopDuration = 2f;                       // 停止する時間（固定）

    protected Transform currentTarget;                       // 現在のターゲット
    protected float fieldOfView = 90f;                       // NPCの視野角
    protected int rayCount = 9;                              // 視野内に飛ばすレイの数

    protected bool isInCombat = false;                       // 撃ち合い中かどうか
    protected float combatStartTime = 0f;                    // 撃ち合い開始時間
    #endregion
    // 移動処理
    public virtual void Move() {
        directionTimer += Time.deltaTime;

        // 壁検知か一定時間で方向転換
        if (WallSearch() || directionTimer >= directionChangeInterval)
        {
            ChangeDirection();
        }

        // 向きを徐々に変更
        if (moveDirection != Vector3.zero)
        {
            Vector3 lookDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
            transform.forward = Vector3.Lerp(transform.forward, lookDirection, Time.deltaTime * 5f);
        }

        // 移動
        transform.position += moveDirection * moveSpd * Time.deltaTime;
    }
    // チート
    public virtual void UpStatus() { }
    // 射撃処理
    public virtual void Shot() {
        Vector3 shootDirection = firePoint.forward;
        float inaccuracy = 1f - hit;

        // 精度に基づいて方向をランダムにずらす
        shootDirection += new Vector3(
            Random.Range(-inaccuracy, inaccuracy),
            Random.Range(-inaccuracy, inaccuracy),
            Random.Range(-inaccuracy, inaccuracy)
        );
        shootDirection.Normalize();

        // 弾の生成と速度設定
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(shootDirection));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        const float bulletSpd = 20000.0f;

        if (rb != null)
        {
            rb.velocity = shootDirection * bulletSpd * Time.deltaTime;
        }

        Debug.Log($"{nameId} が発砲した（精度: {hit * 100f}％）");
    }
    // リスポーン処理
    public virtual void Respwan() {
        StartCoroutine(RespawnRoutine());
    }
    // 敵検知
    public virtual bool EnemySearch()
    {  // 単一レイの敵検知（未使用）
        Ray ray = new Ray(npcCamera.transform.position, npcCamera.transform.forward);
        return Physics.Raycast(ray, out RaycastHit hitInfo, 50f, enemyLayer);
    }
    // 壁検知
    public virtual bool WallSearch()
    {  // 壁検知(前方1m)
        return Physics.Raycast(transform.position, moveDirection, 1f, wallLayer);
    }
    // ダメージを受ける処理
    public virtual void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"{nameId} が {damage} ダメージを受けた！ 残りHP: {hp}");

        if (hp <= 0)
        {
            Die();
        }
    }
    // 死亡処理
    protected virtual void Die()
    {
        Debug.Log($"{nameId} は倒れた");
        // この個体がなくなるまで同じ名前は存在させない
        NameGenerator.ReleaseName(nameId); // 名前の開放
        gameObject.SetActive(false);
    }
    private IEnumerator RespawnRoutine()
    {
        // 復活まで待機
        yield return new WaitForSeconds(resSpd);
        hp = 100;
        transform.position = Vector3.zero;
        gameObject.SetActive(true);
        currentTarget = null;
        Debug.Log($"{nameId} が復活した！");
    }
    // レイでのチェック壁越しで敵を見つけないように
    private bool SearchWithViewRays()
    {
        float halfFOV = fieldOfView / 2f;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = Mathf.Lerp(-halfFOV, halfFOV, i / (rayCount - 1f));
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Vector3 direction = rotation * npcCamera.transform.forward;
            Ray ray = new Ray(npcCamera.transform.position, direction);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 50f))
            {
                // 壁が先にあったら敵は見えない
                if (((1 << hitInfo.collider.gameObject.layer) & wallLayer) != 0)
                {
                    continue;  // 壁に遮られているので無視
                }

                // 敵が見つかったらターゲット設定
                if (((1 << hitInfo.collider.gameObject.layer) & enemyLayer) != 0)
                {
                    currentTarget = hitInfo.transform;
                    return true;
                }
            }
        }
        return false;
    }
    // ターゲットの有効性チェック（例：HPが0か距離が遠いなど）
    private bool IsTargetValid(Transform target)
    {
        if (target == null) return false;

        // 例：ターゲットが死んでいるか判定（Characterスクリプトのhpを参照）
        var character = target.GetComponent<Character>();
        if (character != null && character.hp <= 0) return false;

        // 例：距離が一定以上離れていたら無効
        float maxTargetDistance = 50f;
        if (Vector3.Distance(transform.position, target.position) > maxTargetDistance) return false;

        return true;
    }
    private void AimAndShoot()
    {
        if (currentTarget == null) return;

        // ターゲット方向に徐々に向く
        Vector3 targetDirection = (currentTarget.position - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, new Vector3(targetDirection.x, 0, targetDirection.z), 0.1f);

        // 射撃可能なら発砲
        if (shotTimer >= shotCooldown)
        {
            Shot();
            shotTimer = 0f;

            // 射撃後にターゲットがまだ有効かチェック
            if (currentTarget == null || !IsTargetValid(currentTarget))
            {
                currentTarget = null;
                isInCombat = false;
                Debug.Log($"{nameId} はターゲットを失い撃ち合い終了");
            }
        }
    }
    // 移動時間をランダム設定
    private void SetRandomMoveDuration()
    {
        moveDuration = Random.Range(5f, 10f);
    }
    private void ChangeDirection()
    {
        // ランダム方向を設定
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        directionTimer = 0f;
    }
    // 継承先のスクリプトでの共通スタート
    protected void　CharaSetUp()
    {
        hp = 100;                                 // 初期HP
        nameId = NameGenerator.GetUniqueName();   // 識別用ID
        moveSpd = 3.5f;                           // 移動速度
        hit = 0.95f;                              // 射撃精度
        resSpd = 5f;                              // 復活速度
        anim.Play("Idle");                        // 初期アニメーション
        ChangeDirection();                        // 初期方向
        SetRandomMoveDuration();                  // 最初の移動時間を設定
    }
    // 共通アップデート
    protected void CharaUpdate()
    {
        moveStopTimer += Time.deltaTime;
        shotTimer += Time.deltaTime;

        if (currentTarget != null)
        {
            // 撃ち合い状態開始判定
            if (!isInCombat)
            {
                isInCombat = true;
                combatStartTime = Time.time;
            }

            AimAndShoot();  // ターゲットに向かって攻撃

            // 撃ち合い中は移動しないため、移動処理は行わない
            anim.Play("Idle");
            return;
        }

        // 敵が視野内にいるかチェック（複数レイ飛ばし）
        if (SearchWithViewRays())
        {
            anim.Play("Idle");
            return;
        }

        // ターゲットがいないので撃ち合い状態もリセット
        isInCombat = false;

        // 移動・停止の切り替え処理
        if (isMoving)
        {
            if (moveStopTimer >= moveDuration)
            {
                isMoving = false;
                moveStopTimer = 0f;
            }
            else
            {
                anim.Play("Run");
                Move();
            }
        }
        else
        {
            if (moveStopTimer >= stopDuration)
            {
                isMoving = true;
                moveStopTimer = 0f;
                SetRandomMoveDuration();  // 次の移動時間設定
            }
            anim.Play("Idle");
        }
    }
}