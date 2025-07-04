using UnityEngine;

public class MoneyRelay
{
    private int money = 0; //���Y�p�̕ϐ�
    private int retMoney = 0; // �O���ɑ��鎑�Y
    private float timer = 0; //cooltime�p��timer�ϐ�
    public  float MONEY_COOLTIME = 2f; //MONEY�̃N�[���^�C���錾

    private const int Server_grow = 3;//�T�[�o�[�p�̒萔
    private const int Debug_grow = 30;//�f�o�b�O�p�̒萔
    private const int Sns_grow = 100;//SNS�p�̒萔

    private const int MONEY_MAX = 1000000000; //MONEY�̏��

  

    public void MoneyGrow()//�ʂ̃X�N���v�g�Ōďo��
    {
        var dr = DataRelay.Dr;

        //���Ԍv�Z�p
        if (dr.Famous == 0)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer += Time.deltaTime + 0.15f * (float)dr.Famous * Time.deltaTime;
        }
       
        if (timer >= MONEY_COOLTIME)
        {
            //cooltime���ƂɎ�����萔�����₷����
            money = Calculation.GetMoney(money) +
              dr.Debug_* Debug_grow +
              dr.Server * Server_grow +
              dr.Sns * Sns_grow;

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

    ///
    public void MoneyCalc()
    {
        var dr = DataRelay.Dr;

        //cooltime���ƂɎ�����萔�����₷����
        money = Calculation.GetMoney(money) +
          dr.Debug_ * Debug_grow +
          dr.Server * Server_grow +
          dr.Sns * Sns_grow;

        // �v�Z�Ƃ͕ʂɃR�s�[����
        retMoney = money;

        money = 0;
    }

    // �����̒l
    public int ReturnMoney
    {
        get
        {
            return retMoney;
        }
    }
       
}
