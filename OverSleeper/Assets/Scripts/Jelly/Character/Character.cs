using UnityEngine;
using System.Collections;

public class Character : CharacterBase
{
    [SerializeField] private Transform firePoint;          // �e�𔭎˂���ʒu
    [SerializeField] private GameObject bulletPrefab;      // �e�̃v���n�u
    [SerializeField] private Camera npcCamera;             // ���g�̃J����
    [SerializeField] private LayerMask wallLayer;          // �ǔ���p���C���[
    [SerializeField] private LayerMask enemyLayer;         // �G����p���C���[

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
            ChangeDirection();
        }
        else if (directionTimer >= directionChangeInterval)
        {
            ChangeDirection();
        }

        // �L�����N�^�[�̌�����i�s�����ɃX���[�Y�ɉ�]
        if (moveDirection != Vector3.zero)
        {
            Vector3 lookDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
            transform.forward = Vector3.Lerp(transform.forward, lookDirection, Time.deltaTime * 5f); // 5f�͉�]���x
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
        Debug.Log($"{nameId} �����C����");
    }

    public override void Respwan()
    {
        // �������F�������ĕ���
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(resSpd);
        hp = 100;
        transform.position = Vector3.zero; // �C�ӂ̃��X�|�[���ʒu
        gameObject.SetActive(true);
        Debug.Log($"{nameId} �����������I");
    }
}
