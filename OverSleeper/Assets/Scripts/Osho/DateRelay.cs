
using UnityEngine;

public class DateRelay
{
    private float timer;
    //1�N�i12�����j�̒萔�錾
    private const int MONTH_MAX = 12;
    //Date�̃N�[���^�C��
    public float DATE_COOLTIME = 5.0f; 
   
    public void DateGrow()
    {
        timer += Time.deltaTime;
        if(timer >= DATE_COOLTIME)
        {
            //cooltime���ƂɌ���1���₷�B
            DataRelay.Dr.Month++;
            //12���ɓ��B�����猎��1�ɖ߂��ĔN��1���₷�B
            if (DataRelay.Dr.Month > MONTH_MAX)�@
            {
                DataRelay.Dr.Month = 1;
                DataRelay.Dr.Year++;
            }
            timer = 0;
        }
    }
}
