using UnityEngine;

public class MoveRectSmooth : MonoBehaviour
{
    [Header("�ړ��ݒ�")]
    public Vector2 direction = Vector2.left;  // �ړ�����
    public float speed = 150f;                // �ړ����x�ipx/sec�j

    const float _DELETEPOS = -2000.0f;

    private RectTransform rectTransform;

    // �������F���g��RectTransform���擾
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // �t���[�����Ƃ̈ړ�����
    void Update()
    {
        // �ړ������Ɋ�Â��Ĉʒu���X�V
        rectTransform.anchoredPosition += direction.normalized * speed * Time.deltaTime;

        // �폜�����F���g�̈ʒu���u�폜��I�u�W�F�N�g�v��荶�ɍs������
        if ( rectTransform.anchoredPosition.x < _DELETEPOS)
        {
            Destroy(gameObject); // ���̃I�u�W�F�N�g���폜
        }
    }
}
