
using UnityEngine;

public class MoneyRelay
{
    private int money = 0; //���Y�p�̕ϐ�
    private float timer = 0; //cooltime�p��timer�ϐ�
    public  float MONEY_COOLTIME = 0.5f; //MONEY�̃N�[���^�C���錾
    public void MoneyGrow()//�ʂ̃X�N���v�g�Ōďo��
    {
        //���Ԍv�Z�p
        timer += Time.deltaTime; 
        if (timer >= MONEY_COOLTIME)
        {
            Debug.Log("�ʂ��Ă���");
            //cooltime���ƂɎ�����萔�����₷����
            money = Calculation.GetMoney(money);
            //DateRelay�ɒl��Ԃ��B
            timer = 0;
            DataRelay.Dr.Money += money; 
            money = 0;
        }
    }
       
}
