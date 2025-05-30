using UnityEngine;
using System; // �v�Z�Ɏg��
// �v�Z�̃��\�b�h�����Ă����܂�
public static class Calculation
{
    // �R�X�g�v�Z�Ŏg���萔
    private const int COST_BASE = 100;
    private const float COST_MULTI = 1.35f;

    //�����v�Z�Ŏg���ϐ�
    private const int MONEY = 1; //�萔
    /// <summary>
    /// �w�背�x���ɂ����鎟�̃��x���A�b�v�܂ł̃R�X�g���v�Z���ĕԂ�
    /// </summary>
    /// <param name="currentLevel">���݂̃��x��</param>
    /// <returns>�����x���܂ł̕K�v�R�X�g</returns>
    public static int GetNextLevelCost(int currentLevel)
    {
        float cost = COST_BASE * (float)Math.Pow(COST_MULTI, currentLevel);
        return Mathf.CeilToInt(cost); // �����؂�グ�Đ����ɕϊ�
    }

    //�����v�Z
    public static int GetMoney(int  currentmoney)
    {
        return currentmoney + MONEY; //�����𑝂₷�����ɕύX�@
    }
}
