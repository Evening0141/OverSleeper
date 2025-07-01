using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffObj;

    public float lifeTime = 3f;
    public int damage = 10;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // �G�ȊO�ɖ��������瑦�폜
        if (!other.CompareTag("Character"))
        {
            Destroy(gameObject);
        }
        // ����
        GameObject hitObj;
        // ���������I�u�W�F�N�g���W
        Vector3 hitPos = other.transform.position;
        // �I�t�Z�b�g
        float ofsY = 4.0f;
        hitPos.y = ofsY;

        // CharacterBase ���擾���ă_���[�W��^����
        CharacterBase target = other.GetComponent<CharacterBase>();
        if (target != null)
        {
            target.TakeDamage(damage);
            // �G�t�F�N�g����
            hitObj = Instantiate(hitEffObj, hitPos, Quaternion.identity);
            Destroy(hitObj, lifeTime);
        }
        Debug.Log("HIT");
        Destroy(gameObject);
    }
}
