using UnityEngine;
using UnityEngine.UI;
public class ToolButton : MonoBehaviour
{
    /// <summary>
    /// level,money���e�L�X�g�ɔ��f������
    /// </summary>
    /// <param name="level">ToolData��level</param>
    /// <param name="money">ToolData��money</param>
    /// <param name="levelText">level�̔��f��</param>
    /// <param name="moneyText">money�̔��f��</param>
    protected void SetText(int level, int money, Text levelText, Text moneyText)
    {
        // �R���|�[�l���g�擾
        Transform child_week = transform.Find("LevelText");
        Transform child_money = transform.Find("moneyText");
        levelText = child_week.GetComponentInChildren<Text>();
        moneyText = child_money.GetComponentInChildren<Text>();
        // �\��
        levelText.text = "Lv." + level.ToString();
        moneyText.text = "��p:" + money.ToString() + "��";
    }
}
