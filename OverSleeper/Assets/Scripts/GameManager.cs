using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Field
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

    [Header("UIManager�X�N���v�g"), SerializeField] UIManager manager_UI;

    private static GameManager instance; // Singleton�C���X�^���X
    FadeInOut fade;  // �t�F�[�h�@�\
    #endregion

    /// <summary>
    /// �V�[�g�J�ڂ������̋@�\
    /// </summary>
    #region 
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
        switch (scene.name)�@// enum�̃Z�b�g��������
        {
            case "Title":
                type = SceneType.Title;
                // �R���|�[�l���g�擾�Ƃ��̃X�^�[�g���\�b�h��C�ӂŌĂяo��
                Ready();
                break;
            case "Game":
                type = SceneType.Game;
                // �R���|�[�l���g�擾�Ƃ��̃X�^�[�g���\�b�h��C�ӂŌĂяo��
                Ready();
                // ���̂ݎ��s
                manager_UI.LocalCall_GAME();

                break;
            case "GameOver":
                type = SceneType.GameOver;
                break;
            default:
                Debug.LogWarning("����`�̃V�[��: " + scene.name);
                break;
        }
    }
    #endregion

    private void Start()
    {
        QualitySettings.vSyncCount = 0; // VSync�𖳌���
        Application.targetFrameRate = 60; // 60FPS�ɐݒ�
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

    // �C�ӂŃX�^�[�g���\�b�h����т��������̂�
    private void Ready()
    {
        manager_UI.GeneralCall();
    }

    // �e�V�[�����Ƃ̏���
    #region Scene
    // �^�C�g������Game�ւ̃V�[���J�ڂ�StartKey�X�N���v�g�ɂĎ��s
    private void HandleTitleScene()
    {
        Debug.Log("�^�C�g���V�[���ɑJ�ڂ��܂���");
        manager_UI.UIGeneralUp();

        //fade = FadeInOut.CreateInstance(); // �t�F�[�h�I�u�W�F�N�g�̐���
        //fade.LoadScene("Game");
        // 
    }

    private void HandleGameScene()
    {
        Debug.Log("�Q�[���V�[���ɑJ�ڂ��܂���");
        manager_UI.UIGeneralUp();
        // 
    }

    private void HandleGameOverScene()
    {
        Debug.Log("�Q�[���I�[�o�[�V�[���ɑJ�ڂ��܂���");
        // 
    }
    #endregion
}
