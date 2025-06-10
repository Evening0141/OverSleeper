
using UnityEngine;
using System.Numerics;

public class MoneyRelay
{
    private int money = 0; //���Y�p�̕ϐ�
    private float timer = 0; //cooltime�p��timer�ϐ�
    public  float MONEY_COOLTIME = 0.5f; //MONEY�̃N�[���^�C���錾

    private const int Debug_grow = 100;//�f�o�b�O�p�̒萔
    private const int Server_grow = 10;//�T�[�o�[�p�̒萔
    private const int Sns_grow = 1000;//SNS�p�̒萔
    private const int MONEY_MAX = 1000000000; //MONEY�̏��


    public void MoneyGrow()//�ʂ̃X�N���v�g�Ōďo��
    {
        //���Ԍv�Z�p
        timer += Time.deltaTime; 
        if (timer >= MONEY_COOLTIME)
        {
            Debug.Log("�ʂ��Ă���");
            //cooltime���ƂɎ�����萔�����₷����
            money = Calculation.GetMoney(money) + DataRelay.Dr.Debug_* Debug_grow + DataRelay.Dr.Server* Server_grow +DataRelay.Dr.Sns* Sns_grow;

            // ���݂̎������擾
            int currentMoney = DataRelay.Dr.Money;
            // 10���̏�� MONEY_MAX�𒴂��Ȃ��悤�Ƀ`�F�b�N
            if (currentMoney <= MONEY_MAX - money)
            {
                DataRelay.Dr.Money += money;
            }
            else
            {
                DataRelay.Dr.Money = MONEY_MAX;//�������ꍇMONEY_MAX�ɌŒ�
            }


            timer = 0;
            money = 0;
        }
    }
       
}
