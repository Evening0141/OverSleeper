using UnityEngine;
using UnityEngine.UI;    // UI�֘A
using UnityEngine.EventSystems; // �}�E�X����֘A

public class Item : MonoBehaviour,
    IBeginDragHandler, // �h���b�O�J�n
    IDragHandler,      // �h���b�O��
    IEndDragHandler    // �h���b�O�I�� 
{
    private Image image;
    private RectTransform rectTransform;
    private bool isDrag = false;
    private CanvasGroup cGroup;
    void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        cGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if(isDrag)
        {
            if(Input.GetMouseButtonDown(1))
            {
                rectTransform.localEulerAngles +=
                    new Vector3(0, 0, -90);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
        // ���C�ђ�
        cGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDrag = true;
        // �|�C���^�̈ʒu�ɕ␳
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        // ���C��h��
        cGroup.blocksRaycasts = true;
    }

}
