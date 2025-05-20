using UnityEngine;
using UnityEngine.UI;

public class ServerKey : MonoBehaviour,IChildBehavior
{
    Text levelText;  // ���x����\������e�L�X�g
    Text moneyText;  // ��p��\������e�L�X�g

    private float cost;

    // �C���^�[�t�F�[�X
    // �ݔ��{�^���̒��̃T�[�o�[�@�\�ł�
    public void Execute()
    {

    }

    // �e�L�X�g���f

    private void Awake()
    {
        Cost();
        // �R���|�[�l���g�擾
        Transform child_week = transform.Find("LevelText");
        Transform child_money = transform.Find("moneyText");
        levelText = child_week.GetComponentInChildren<Text>();
        moneyText = child_money.GetComponentInChildren<Text>();
        // �\��
        levelText.text = "Lv." + DataRelay.Dr.server.ToString();
        moneyText.text = "��p:" + cost.ToString() + "��";
    }

    // �R�X�g�v�Z
    private void Cost()
    {
        cost = (float)DataRelay.Dr.server * 1.3f * 100.0f;
    }
}
