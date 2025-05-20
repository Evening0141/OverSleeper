using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalUIManager : MonoBehaviour
{
    private TextSpacer[] spaceCreate; // �󔒕�����ǉ�����X�N���v�g

    // UnityEditor�̏���
    #region DEBUG
    // UIManager��Title�V�[������̌p��������̂�
    // �����̓f�o�b�O�p�ɒu���Ă���܂�
#if UNITY_EDITOR
    private void Start()
    {
        // ���s���ɓ���I�u�W�F�N�g��T��
        GameObject debugBlocker = GameObject.Find("UIManager");

        if (debugBlocker != null)
        {
            Debug.Log("�����͎��s���܂���");
            return;  // �������~�߂�
        }

        // �V�[�����̏�����ǂݍ���
        spaceCreate = FindObjectsOfType<TextSpacer>(true);
        // �������s
        foreach (var text in spaceCreate)
        {
            text.TextSpace();
        }
    }
    private void Update()
    {
        
    }
#endif
    #endregion


    // Main�����͂�������
    // UIManager�ŌĂяo���`�ō쐬
    public void LocalStart()
    {
        // �V�[�����̏�����ǂݍ���
        spaceCreate = FindObjectsOfType<TextSpacer>(true);
        // �������s
        foreach (var text in spaceCreate)
        {
            text.TextSpace();
        }
    }
}
