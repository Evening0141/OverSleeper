
using UnityEngine;

public class MoneyRelay
{
    public int money = 0; //���Y�p�̕ϐ�
    public float timer = 0; //cooltime�p��timer�ϐ�
    private const float cool_time = 5.0f; //cooltime
    public void MoneyGrow()//�ʂ̃X�N���v�g�Ōďo��
    {
        timer += Time.deltaTime; //���Ԍv�Z�p
        if (timer >= cool_time)
        {
            Debug.Log("�ʂ��Ă���");
            money = Calculation.GetMoney(money);
            DataRelay.Dr.Money += money;
            timer = 0;
            money = 0;
        }
    }
       
}
