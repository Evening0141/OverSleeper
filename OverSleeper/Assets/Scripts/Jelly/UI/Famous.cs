using UnityEngine;

public class Famous
{
    DataRelay dr = DataRelay.Dr;

    private int famous;

    // ���Ԍv�Z�p
    private const float _TIME = 70.0f;
    private float cntTimer = _TIME;

    // �`�[�^�[�o�����̌v�Z�p
    private float cntTimerCheatON=_TIME;

    public void FamousCount()
    {
        // �l��������B
        // DataRelay����Famous�͏���Ɖ��������߂��Ă邩�炻������ɂ����Œ�������
        if (famous != dr.Famous)
        {
            famous = dr.Famous;
        }
        if (!dr.IsCheat)
        {
            cntTimer -= Time.deltaTime;
            if (cntTimer <= 0)
            {
                // ���ԍăZ�b�g
                cntTimer = _TIME;
                famous++;
                dr.Famous = famous;
            }
        }
        // �o��������ςȂ����Ɛl�C�x��������
        else
        {
            cntTimerCheatON -= Time.deltaTime;
            if (cntTimerCheatON <= 0)
            {
                // ���ԍăZ�b�g
                cntTimerCheatON = _TIME;
                famous--;
                dr.Famous = famous;
            }
        }
    }
}
