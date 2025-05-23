using UnityEngine;
using UnityEngine.UI;

public class DevButton : MonoBehaviour
{
    /// <summary>
    /// week,money���e�L�X�g�ɔ��f������
    /// </summary>
    /// <param name="week">devData��week</param>
    /// <param name="money">devData��money</param>
    /// <param name="weekText">week�̔��f��</param>
    /// <param name="moneyText">money�̔��f��</param>
    protected void SetText(int week,int money,Text weekText,Text moneyText)
    {
        // �R���|�[�l���g�擾
        Transform child_week = transform.Find("weekText");
        Transform child_money = transform.Find("moneyText");
        weekText = child_week.GetComponentInChildren<Text>();
        moneyText = child_money.GetComponentInChildren<Text>();
        // �\��
        weekText.text = "����:" + week.ToString() + "�T";
        moneyText.text = "��p:" + money.ToString() + "��";
    }
}
