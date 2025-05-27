using UnityEngine;
using UnityEngine.UI;

public class ChangePanelBase : MonoBehaviour
{
    [Header("�؂�ւ���ɔ�\���ɂ���Image"), SerializeField] Image beforeImg;
    [Header("�؂�ւ���ɕ\���ɂ���Image"), SerializeField] Image afterImg;

    // �J���[�ϐ�
    const float beforeColAlpha = 0.0f;
    const float afterColAlpha = 0.01f;

    public void ChangePanel()
    {
        // �}�X�N�̃��l�𗘗p���ăp�l���̐؂�ւ�������
        beforeImg.color = new Color(1, 1, 1, beforeColAlpha);
        afterImg.color = new Color(1, 1, 1, afterColAlpha);
    }
   

}
