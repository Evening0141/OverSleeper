using UnityEngine;
using UnityEngine.UI;

public class ServerKey : MonoBehaviour,IChildBehavior
{
    [SerializeField] ToolData toolData; // �f�[�^�̎擾
    Text levelText;  // ���x����\������e�L�X�g
    Text moneyText;  // ��p��\������e�L�X�g

    // �C���^�[�t�F�[�X
    // �ݔ��{�^���̒��̃T�[�o�[�@�\�ł�
    public void Execute()
    {

    }

    // �e�L�X�g���f

    private void Awake()
    {
        // �R���|�[�l���g�擾
        Transform child_week = transform.Find("LevelText");
        Transform child_money = transform.Find("moneyText");
        levelText = child_week.GetComponentInChildren<Text>();
        moneyText = child_money.GetComponentInChildren<Text>();
        // �\��
       // levelText.text = "Lv." + level.ToString();
       // moneyText.text = "��p:" + money.ToString() + "��";
    }
}
