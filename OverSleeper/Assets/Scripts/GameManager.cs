using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Field
    [Header("UIManager�X�N���v�g"),SerializeField] UIManager manager_UI;

    private static GameManager instance; // Singleton�C���X�^���X

    FadeInOut fade;  // �t�F�[�h�@�\

    // �V�[�����Ƃɋ@�\��ύX���Ă���
    private enum SceneType
    {
        Title,    // �^�C�g���V�[��
        Game,     // �Q�[���V�[��
        GameOver, // �Q�[���I�[�o�[�V�[��
        Unknown   // �z��O�̃V�[�����p
    }

    // �^�C�g������͂��߂�
    SceneType type = SceneType.Title;
    #endregion

    void Awake()
    {
        // Singleton�p�^�[��: ����UIManager�����ɑ��݂���ꍇ�A���݂̃C���X�^���X���폜
        if (instance != null)
        {
            Destroy(gameObject); // �d�������C���X�^���X���폜
        }
        else
        {
            instance = this; // ����̂݃C���X�^���X��ݒ�
            DontDestroyOnLoad(gameObject); // ���̃I�u�W�F�N�g���V�[�����ύX����Ă��j�����Ȃ�

            // �V�[���ǂݍ��ݎ��̃C�x���g�o�^
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    void OnDestroy()
    {
        // �C�x���g�����i�O�̂��߁j
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
    // �V�[���ǂݍ��݌�ɌĂ΂��
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �R���|�[�l���g�擾�Ƃ��̃X�^�[�g���\�b�h��C�ӂŌĂяo��
        Ready();
        
        switch (scene.name)�@// enum�̃Z�b�g��������
        {
            case "Title":
                type = SceneType.Title;
                break;
            case "Game":
                type = SceneType.Game; 
                break;
            case "GameOver":
                type = SceneType.GameOver;
                break;
            default:
                Debug.LogWarning("����`�̃V�[��: " + scene.name);
                break;
        }
    }
    private void Update()
    {
        switch (type)
        {
            // �^�C�g���V�[���̏���
            case SceneType.Title:
                HandleTitleScene();
                break;
            // �Q�[���V�[���̏���
            case SceneType.Game:
                HandleGameScene();
                break;
            // �Q�[���I�[�o�[�V�[���̏���
            case SceneType.GameOver:
                HandleGameOverScene();
                break;
            default:
                break;
        }

    }

    // �e�V�[�����Ƃ̏���
    private void HandleTitleScene()
    {
        Debug.Log("�^�C�g���V�[���ɑJ�ڂ��܂���");
       // fade = FadeInOut.CreateInstance(); // �t�F�[�h�I�u�W�F�N�g�̐���
       // fade.LoadScene("Game");
        // 
    }

    private void HandleGameScene()
    {
        Debug.Log("�Q�[���V�[���ɑJ�ڂ��܂���");
        // 
    }

    private void HandleGameOverScene()
    {
        Debug.Log("�Q�[���I�[�o�[�V�[���ɑJ�ڂ��܂���");
        // 
    }


    // �C�ӂŃX�^�[�g���\�b�h����т��������̂�
    private void Ready()
    {
        manager_UI.UIStart();
    }
}
