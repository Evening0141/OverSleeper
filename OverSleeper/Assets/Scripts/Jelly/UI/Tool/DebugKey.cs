using UnityEngine;
using UnityEngine.UI;

public class DebugKey : MonoBehaviour, IChildBehavior
{
    Text levelText;  // ���x����\������e�L�X�g
    Text moneyText;  // ��p��\������e�L�X�g

    private float cost;

    private int level = -1;

    // �e�L�X�g���f

    private void Awake()
    {
        level = DataRelay.Dr.Debug_;
        // �ꉞ�`�F�b�N
        if (level == -1)
        {
            Debug.Log("�f�[�^�𐳂����󂯎��܂���ł����B");
            return;
        }
        Cost();
        // �R���|�[�l���g�擾
        Transform child_week = transform.Find("LevelText");
        Transform child_money = transform.Find("moneyText");
        levelText = child_week.GetComponentInChildren<Text>();
        moneyText = child_money.GetComponentInChildren<Text>();
        // �\��
        levelText.text = "Lv." + level.ToString();
        moneyText.text = "��p:" + cost.ToString() + "��";
    }

    // �R�X�g�v�Z
    private void Cost()
    {
        cost = Calculation.GetNextLevelCost(level);
    }

    // �C���^�[�t�F�[�X
    // �ݔ��{�^���̒��̃T�[�o�[�@�\�ł�
    public void Execute()
    {

    }

}
