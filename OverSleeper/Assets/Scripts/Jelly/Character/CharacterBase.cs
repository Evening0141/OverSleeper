using UnityEngine;
using System.Collections;

public class CharacterBase : MonoBehaviour
{
    // �ϐ��܂Ƃ�
    #region field
    protected int hp;     �@�@ // �v���C���[HP
    protected string nameId;   // �v���C���[��
    protected float hit;  �@�@ // �ˌ����x
    protected float moveSpd;   // �ړ����x
    protected float resSpd;    // ���X�|�[���N�[���^�C��
    protected bool IsC = false; // �`�[�g�g�p   

    [Header("��{�ݒ�")]
    [SerializeField] protected Transform firePoint;         // �e�̔��ˈʒu
    [SerializeField] protected GameObject bulletPrefab;     // �e�̃v���n�u
    [SerializeField] protected Camera npcCamera;            // NPC�̎��E�J����
    [SerializeField] protected LayerMask wallLayer;         // �Ǘp���C���[
    [SerializeField] protected LayerMask enemyLayer;        // �G�p���C���[
    [SerializeField] protected Animator anim;               // �A�j���[�^�[

    protected Vector3 moveDirection;                         // �ړ�����
    protected float directionChangeInterval = 5f;            // �ړ������ύX�Ԋu
    protected float directionTimer = 0f;                     // �ړ������ύX�p�^�C�}�[
    protected float shotCooldown = 1f;                       // �e�̃N�[���_�E��
    protected float shotTimer = 0f;                          // �e���˃^�C�}�[

    protected bool isMoving = true;                          // �ړ������ǂ���
    protected float moveStopTimer = 0f;                      // ��~�E�ړ��ؑւ̂��߂̃^�C�}�[
    protected float moveDuration = 0f;                       // ���݂́u�ړ����ԁv
    protected float stopDuration = 2f;                       // ��~���鎞�ԁi�Œ�j

    protected Transform currentTarget;                       // ���݂̃^�[�Q�b�g
    protected float fieldOfView = 90f;                       // NPC�̎���p
    protected int rayCount = 9;                              // ������ɔ�΂����C�̐�

    protected bool isInCombat = false;                       // �������������ǂ���
    protected float combatStartTime = 0f;                    // ���������J�n����
    #endregion
    // �ړ�����
    public virtual void Move() {
        directionTimer += Time.deltaTime;

        // �ǌ��m����莞�Ԃŕ����]��
        if (WallSearch() || directionTimer >= directionChangeInterval)
        {
            ChangeDirection();
        }

        // ���������X�ɕύX
        if (moveDirection != Vector3.zero)
        {
            Vector3 lookDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
            transform.forward = Vector3.Lerp(transform.forward, lookDirection, Time.deltaTime * 5f);
        }

        // �ړ�
        transform.position += moveDirection * moveSpd * Time.deltaTime;
    }
    // �`�[�g
    public virtual void UpStatus() { }
    // �ˌ�����
    public virtual void Shot() {
        Vector3 shootDirection = firePoint.forward;
        float inaccuracy = 1f - hit;

        // ���x�Ɋ�Â��ĕ����������_���ɂ��炷
        shootDirection += new Vector3(
            Random.Range(-inaccuracy, inaccuracy),
            Random.Range(-inaccuracy, inaccuracy),
            Random.Range(-inaccuracy, inaccuracy)
        );
        shootDirection.Normalize();

        // �e�̐����Ƒ��x�ݒ�
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(shootDirection));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        const float bulletSpd = 20000.0f;

        if (rb != null)
        {
            rb.velocity = shootDirection * bulletSpd * Time.deltaTime;
        }

        Debug.Log($"{nameId} �����C�����i���x: {hit * 100f}���j");
    }
    // ���X�|�[������
    public virtual void Respwan() {
        StartCoroutine(RespawnRoutine());
    }
    // �G���m
    public virtual bool EnemySearch()
    {  // �P�ꃌ�C�̓G���m�i���g�p�j
        Ray ray = new Ray(npcCamera.transform.position, npcCamera.transform.forward);
        return Physics.Raycast(ray, out RaycastHit hitInfo, 50f, enemyLayer);
    }
    // �ǌ��m
    public virtual bool WallSearch()
    {  // �ǌ��m(�O��1m)
        return Physics.Raycast(transform.position, moveDirection, 1f, wallLayer);
    }
    // �_���[�W���󂯂鏈��
    public virtual void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"{nameId} �� {damage} �_���[�W���󂯂��I �c��HP: {hp}");

        if (hp <= 0)
        {
            Die();
        }
    }
    // ���S����
    protected virtual void Die()
    {
        Debug.Log($"{nameId} �͓|�ꂽ");
        // ���̌̂��Ȃ��Ȃ�܂œ������O�͑��݂����Ȃ�
        NameGenerator.ReleaseName(nameId); // ���O�̊J��
        gameObject.SetActive(false);
    }
    private IEnumerator RespawnRoutine()
    {
        // �����܂őҋ@
        yield return new WaitForSeconds(resSpd);
        hp = 100;
        transform.position = Vector3.zero;
        gameObject.SetActive(true);
        currentTarget = null;
        Debug.Log($"{nameId} �����������I");
    }
    // ���C�ł̃`�F�b�N�ǉz���œG�������Ȃ��悤��
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
                // �ǂ���ɂ�������G�͌����Ȃ�
                if (((1 << hitInfo.collider.gameObject.layer) & wallLayer) != 0)
                {
                    continue;  // �ǂɎՂ��Ă���̂Ŗ���
                }

                // �G������������^�[�Q�b�g�ݒ�
                if (((1 << hitInfo.collider.gameObject.layer) & enemyLayer) != 0)
                {
                    currentTarget = hitInfo.transform;
                    return true;
                }
            }
        }
        return false;
    }
    // �^�[�Q�b�g�̗L�����`�F�b�N�i��FHP��0�������������Ȃǁj
    private bool IsTargetValid(Transform target)
    {
        if (target == null) return false;

        // ��F�^�[�Q�b�g������ł��邩����iCharacter�X�N���v�g��hp���Q�Ɓj
        var character = target.GetComponent<Character>();
        if (character != null && character.hp <= 0) return false;

        // ��F���������ȏ㗣��Ă����疳��
        float maxTargetDistance = 50f;
        if (Vector3.Distance(transform.position, target.position) > maxTargetDistance) return false;

        return true;
    }
    private void AimAndShoot()
    {
        if (currentTarget == null) return;

        // �^�[�Q�b�g�����ɏ��X�Ɍ���
        Vector3 targetDirection = (currentTarget.position - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, new Vector3(targetDirection.x, 0, targetDirection.z), 0.1f);

        // �ˌ��\�Ȃ甭�C
        if (shotTimer >= shotCooldown)
        {
            Shot();
            shotTimer = 0f;

            // �ˌ���Ƀ^�[�Q�b�g���܂��L�����`�F�b�N
            if (currentTarget == null || !IsTargetValid(currentTarget))
            {
                currentTarget = null;
                isInCombat = false;
                Debug.Log($"{nameId} �̓^�[�Q�b�g���������������I��");
            }
        }
    }
    // �ړ����Ԃ������_���ݒ�
    private void SetRandomMoveDuration()
    {
        moveDuration = Random.Range(5f, 10f);
    }
    private void ChangeDirection()
    {
        // �����_��������ݒ�
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        directionTimer = 0f;
    }
    // �p����̃X�N���v�g�ł̋��ʃX�^�[�g
    protected void�@CharaSetUp()
    {
        hp = 100;                                 // ����HP
        nameId = NameGenerator.GetUniqueName();   // ���ʗpID
        moveSpd = 3.5f;                           // �ړ����x
        hit = 0.95f;                              // �ˌ����x
        resSpd = 5f;                              // �������x
        anim.Play("Idle");                        // �����A�j���[�V����
        ChangeDirection();                        // ��������
        SetRandomMoveDuration();                  // �ŏ��̈ړ����Ԃ�ݒ�
    }
    // ���ʃA�b�v�f�[�g
    protected void CharaUpdate()
    {
        moveStopTimer += Time.deltaTime;
        shotTimer += Time.deltaTime;

        if (currentTarget != null)
        {
            // ����������ԊJ�n����
            if (!isInCombat)
            {
                isInCombat = true;
                combatStartTime = Time.time;
            }

            AimAndShoot();  // �^�[�Q�b�g�Ɍ������čU��

            // �����������͈ړ����Ȃ����߁A�ړ������͍s��Ȃ�
            anim.Play("Idle");
            return;
        }

        // �G��������ɂ��邩�`�F�b�N�i�������C��΂��j
        if (SearchWithViewRays())
        {
            anim.Play("Idle");
            return;
        }

        // �^�[�Q�b�g�����Ȃ��̂Ō���������Ԃ����Z�b�g
        isInCombat = false;

        // �ړ��E��~�̐؂�ւ�����
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
                SetRandomMoveDuration();  // ���̈ړ����Ԑݒ�
            }
            anim.Play("Idle");
        }
    }
}