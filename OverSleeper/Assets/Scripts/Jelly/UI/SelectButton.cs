using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image selImg; // �I�𒆂ɕ\������I�u�W�F�N�g

    // �q�I�u�W�F�N�g�̃C���^�[�t�F�[�X�擾�p
    IChildBehavior child;

    private bool isHolding = false;

    #region Interface
    public void OnPointerEnter(PointerEventData eventData)
    {
        selImg.enabled = true;   // �\��
        isHolding = true;        // �G��Ă���
        // SE�Z�b�g
        DataRelay.Dr.Data_SE = DataRelay.SE_Name.Enter;
        Debug.Log("UI�ɐG�ꂽ");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selImg.enabled = false;�@// ��\��
        isHolding = false;       // �G��Ă��Ȃ�
        Debug.Log("UI���痣�ꂽ");
    }
    #endregion

    /// <summary>
    /// �����Manager�ɂČĂяo�����s
    /// </summary>
    public void ButtonStart()
    {
        // �q�I�u�W�F�N�g�̒����� IChildBehavior ��T��
        child = GetComponentInChildren<IChildBehavior>();
    }

    /// <summary>
    /// �����Manager�ɂČĂяo�����s
    /// </summary>
    public void ButtonUpdate()
    {
        if (isHolding)
        {
            if (child != null)
            {
                // SE�Z�b�g
                DataRelay.Dr.Data_SE = DataRelay.SE_Name.Click;

                // ���s
                child.Execute();
            }
            else
            {
                Debug.LogWarning("�q�� IChildBehavior �����������X�N���v�g��������܂���");
                Debug.Log(this.gameObject.name);
            }
        }
    }
}
