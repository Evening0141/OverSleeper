using UnityEngine;
using System.Collections;

public class Character : CharacterBase
{
    [Header("基本設定")]
    [SerializeField] private Transform firePoint;         // 弾の発射位置
    [SerializeField] private GameObject bulletPrefab;     // 弾のプレハブ
    [SerializeField] private Camera npcCamera;            // NPCの視界カメラ
    [SerializeField] private LayerMask wallLayer;         // 壁用レイヤー
    [SerializeField] private LayerMask enemyLayer;        // 敵用レイヤー
    [SerializeField] private Animator anim;               // アニメーター

    private Vector3 moveDirection;                         // 移動方向
    private float directionChangeInterval = 5f;           // 移動方向変更間隔
    private float directionTimer = 0f;                     // 移動方向変更用タイマー
    private float shotCooldown = 1f;                       // 弾のクールダウン
    private float shotTimer = 0f;                          // 弾発射タイマー

    private bool isMoving = true;                          // 移動中かどうか
    private float moveStopTimer = 0f;                      // 停止・移動切替のためのタイマー
    private float moveDuration = 0f;                       // 現在の「移動時間」
    private float stopDuration = 2f;                       // 停止する時間（固定）

    private Transform currentTarget;                       // 現在のターゲット
    private float fieldOfView = 90f;                       // NPCの視野角
    private int rayCount = 9;                              // 視野内に飛ばすレイの数

    private bool isInCombat = false;                       // 撃ち合い中かどうか
    private float combatStartTime = 0f;                    // 撃ち合い開始時間

    private void Start()
    {
        hp = 100;
        nameId = "NPC_" + Random.Range(1, 1000);          // 識別用ID
        moveSpd = 3.5f;                                    // 移動速度
        hit = 0.95f;                                       // 射撃精度
        resSpd = 5f;                                       // 復活速度
        anim.Play("Idle");
        ChangeDirection();
        SetRandomMoveDuration();                           // 最初の移動時間を設定
    }

    private void Update()
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

    public override void Move()
    {
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

    private void ChangeDirection()
    {
        // ランダム方向を設定
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        directionTimer = 0f;
    }

    public override bool WallSearch()
    {
        // 壁検知(前方1m)
        return Physics.Raycast(transform.position, moveDirection, 1f, wallLayer);
    }

    public override bool EnemySearch()
    {
        // 単一レイの敵検知（未使用）
        Ray ray = new Ray(npcCamera.transform.position, npcCamera.transform.forward);
        return Physics.Raycast(ray, out RaycastHit hitInfo, 50f, enemyLayer);
    }

    private bool SearchWithViewRays()
    {
        // 視野角内に複数レイを飛ばして敵検知
        float halfFOV = fieldOfView / 2f;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = Mathf.Lerp(-halfFOV, halfFOV, i / (rayCount - 1f));
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Vector3 direction = rotation * npcCamera.transform.forward;
            Ray ray = new Ray(npcCamera.transform.position, direction);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 50f, enemyLayer))
            {
                currentTarget = hitInfo.transform;  // ターゲット登録
                return true;
            }
        }
        return false;
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

    public override void Shot()
    {
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

    public override void Respwan()
    {
        StartCoroutine(RespawnRoutine());
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

    // 移動時間をランダム設定
    private void SetRandomMoveDuration()
    {
        moveDuration = Random.Range(5f, 10f);
    }
}
