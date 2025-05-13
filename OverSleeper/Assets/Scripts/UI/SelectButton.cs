using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image selImg; // �I�𒆂ɕ\������I�u�W�F�N�g

    // �q�I�u�W�F�N�g�̃C���^�[�t�F�[�X�擾�p
    IChildBehavior child;

    private bool isHolding = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // �\��
        selImg.enabled = true;
        isHolding = true;
        Debug.Log("UI�ɐG�ꂽ");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ��\��
        selImg.enabled = false;
        isHolding = false;
        Debug.Log("UI���痣�ꂽ");
    }

    public void ButtonStart()
    {
        Debug.Log("AAA");
        // �q�I�u�W�F�N�g�̒����� IChildBehavior ��T��
        child = GetComponentInChildren<IChildBehavior>();
    }

    public void ButtonUpdate()
    {
        if (isHolding)
        {
            if (child != null)
            {
                child.Execute();
            }
            else
            {
                Debug.LogWarning("�q�� IChildBehavior �����������X�N���v�g��������܂���");
            }
        }
    }

}
