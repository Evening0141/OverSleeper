using UnityEngine;
using System.Collections;

public class Character : CharacterBase
{
    [SerializeField] private Transform firePoint;          // 弾を発射する位置
    [SerializeField] private GameObject bulletPrefab;      // 弾のプレハブ
    [SerializeField] private Camera npcCamera;             // 自身のカメラ
    [SerializeField] private LayerMask wallLayer;          // 壁判定用レイヤー
    [SerializeField] private LayerMask enemyLayer;         // 敵判定用レイヤー

    private Vector3 moveDirection;
    private float directionChangeInterval = 3f;
    private float directionTimer = 0f;
    private float shotCooldown = 1f;
    private float shotTimer = 0f;

    private void Start()
    {
        hp = 100;
        nameId = "NPC_" + Random.Range(1, 1000);
        moveSpd = 3f;
        hit = 0.95f;
        resSpd = 5f;

        ChangeDirection();
    }

    private void Update()
    {
        Move();
        shotTimer += Time.deltaTime;
    }

    public override void Move()
    {
        directionTimer += Time.deltaTime;

        if (WallSearch())
        {
            // 壁を検知したらランダムな方向に変更
            ChangeDirection();
        }
        else if (directionTimer >= directionChangeInterval)
        {
            ChangeDirection();
        }

        transform.position += moveDirection * moveSpd * Time.deltaTime;

        if (EnemySearch())
        {
            AimAndShoot();
        }
    }

    private void ChangeDirection()
    {
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        directionTimer = 0f;
    }

    public override bool WallSearch()
    {
        return Physics.Raycast(transform.position, moveDirection, 1f, wallLayer);
    }

    public override bool EnemySearch()
    {
        Ray ray = new Ray(npcCamera.transform.position, npcCamera.transform.forward);
        return Physics.Raycast(ray, out RaycastHit hitInfo, 50f, enemyLayer);
    }

    private void AimAndShoot()
    {
        Ray ray = new Ray(npcCamera.transform.position, npcCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 50f, enemyLayer))
        {
            Vector3 targetDirection = (hitInfo.point - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, new Vector3(targetDirection.x, 0, targetDirection.z), 0.1f);

            if (shotTimer >= shotCooldown)
            {
                Shot();
                shotTimer = 0f;
            }
        }
    }

    public override void Shot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * 20f;
        }
        Debug.Log($"{nameId} が発砲した");
    }

    public override void Respwan()
    {
        // 仮実装：少しして復活
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(resSpd);
        hp = 100;
        transform.position = Vector3.zero; // 任意のリスポーン位置
        gameObject.SetActive(true);
        Debug.Log($"{nameId} が復活した！");
    }
}
