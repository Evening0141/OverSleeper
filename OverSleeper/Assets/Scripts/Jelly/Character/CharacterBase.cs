using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected int hp;     �@�@  // �v���C���[HP
    protected string nameId;  // �v���C���[��
    protected float hit;  �@�@// �ˌ����x
    protected float moveSpd;   // �ړ����x
    protected float resSpd;    // ���X�|�[���N�[���^�C��

    // �ړ�����
    public virtual void Move() {}

    // �ˌ�����
    public virtual void Shot() {}

    // ���X�|�[������
    public virtual void Respwan() {}

    // �G���m
    public virtual bool EnemySearch() { return false; }

    // �ǌ��m
    public virtual bool WallSearch() { return false; }

    // �`�[�g
    public virtual void UpStatus() {}

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
        // �����Ɏ��S���̏����i�A�j���[�V�����⃊�X�|�[���j���L�q
        gameObject.SetActive(false);
    }
}