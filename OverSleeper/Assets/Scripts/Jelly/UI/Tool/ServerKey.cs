using UnityEngine;
using UnityEngine.UI;

public class ServerKey : MonoBehaviour,IChildBehavior
{
    Text levelText;  // ���x����\������e�L�X�g
    Text moneyText;  // ��p��\������e�L�X�g

    private int cost;
    private int level = -1;

    // �e�L�X�g���f

    private void Awake()
    {
        Cost();
        // �ꉞ�`�F�b�N
        if (level == -1) {
            Debug.Log("�f�[�^�𐳂����󂯎��܂���ł����B");
            return; }
        // �R���|�[�l���g�擾
        Transform child_week = transform.Find("LevelText");
        Transform child_money = transform.Find("moneyText");
        levelText = child_week.GetComponentInChildren<Text>();
        moneyText = child_money.GetComponentInChildren<Text>();
        // �\��
        levelText.text = "Lv." + level.ToString();
        moneyText.text = "��p:" + cost.ToString("N0") + "��";
    }

    // �R�X�g�v�Z
    private void Cost()
    {
        level = DataRelay.Dr.Server;
        // �T�[�o�[�������������ݒ肵�Ă���
        cost = Calculation.GetNextLevelCost(level);
    }

    // �C���^�[�t�F�[�X
    // �ݔ��{�^���̒��̃T�[�o�[�@�\�ł�
    public void Execute()
    {
        Debug.Log("���x���A�b�v");

        if (cost<=DataRelay.Dr.Money)
        {
            // ���x���A�b�v
            DataRelay.Dr.Server++;
            DataRelay.Dr.Money -= cost;
            Cost();
            // �\��
            levelText.text = "Lv." + level.ToString();
            moneyText.text = "��p:" + cost.ToString("N0") + "��";
        }
    }
}
