using UnityEngine;
using UnityEngine.UI;    // UI�֘A
using UnityEngine.EventSystems; // �}�E�X����֘A

// �}�X�̃v���O����
public class Cell : MonoBehaviour,
    IPointerEnterHandler,   // �}�E�X�J�[�\�������킹����
    IPointerExitHandler,    // �}�E�X�J�[�\�����͂Ȃ�����
    IPointerClickHandler    // �}�E�X�N���b�N������
{
    public int x, y;                     // ���g�̍��W
    public bool filled = false;          // �}�X�ɐݒu����Ă��邩

    private Image imgCmp;                // �摜�̃R���|�[�l���g
    private Color defCol = Color.white;  // ��{�F
    private Color selCol = Color.yellow; // �I�����ꂽ��
    private Color fillCol = Color.gray;  // �ݒu�ς݂̎��̐F

    // ��
    public GameObject itemPre;  // �A�C�e�������Ŏ�������

    private void Awake()
    {
        imgCmp = GetComponent<Image>();
    }
    public void SetColor(Color setCol)
    {
        if (imgCmp != null) { imgCmp.color = setCol; }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetColor(selCol);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        SetColor(filled == true? fillCol:defCol);
    }
    public  void OnPointerClick(PointerEventData eventData)
    {
        if(!filled)
        {
            filled = true;
            SetColor(fillCol);

            // ���ŃA�C�e�����}�X�̏ꏊ�ɒu��
            RectTransform canvasRect = transform.root.GetComponent<RectTransform>();
            Instantiate(itemPre, transform.position, Quaternion.identity, canvasRect);
#if false
            // �Z���̍��W
            RectTransform cellRect = GetComponent<RectTransform>();
            // �Z���̃��[���h���W���X�N���[�����W�ɕϊ�
            Vector2 screenPos = 
                RectTransformUtility.WorldToScreenPoint(null, cellRect.position);
            // �L�����o�X�̍��W
            RectTransform canvasRect = transform.root.GetComponent<RectTransform>();
            // �X�N���[�����W����L�����o�X�̃��[�J�����W�ɕϊ�
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect, screenPos, null, out Vector2 localPoint);
            // �A�C�e������
            GameObject item = Instantiate(itemPre, canvasRect);
            RectTransform itemRect = item.GetComponent<RectTransform>();

            itemRect.anchoredPosition = localPoint;
            itemRect.localScale = Vector3.one;
#endif
        }
    }

}
