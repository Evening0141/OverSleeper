
using UnityEngine;

public class MoneyRelay
{
    private int money = 0; //���Y�p�̕ϐ�
    private float timer = 0; //cooltime�p��timer�ϐ�
    public  float MONEY_COOLTIME = 5f; //MONEY�̃N�[���^�C���錾

    private const int Debug_grow = 100;//�f�o�b�O�p�̒萔
    private const int Server_grow = 10;//�T�[�o�[�p�̒萔
    private const int Sns_grow = 1000;//SNS�p�̒萔


    public void MoneyGrow()//�ʂ̃X�N���v�g�Ōďo��
    {
        //���Ԍv�Z�p
        timer += Time.deltaTime; 
        if (timer >= MONEY_COOLTIME)
        {
            Debug.Log("�ʂ��Ă���");
            //cooltime���ƂɎ�����萔�����₷����
            money = Calculation.GetMoney(money) + DataRelay.Dr.Debug_* Debug_grow + DataRelay.Dr.Server* Server_grow +DataRelay.Dr.Sns* Sns_grow;
            //DateRelay�ɒl��Ԃ��B
            timer = 0;
            DataRelay.Dr.Money += money; 
            money = 0;
        }
    }
       
}
