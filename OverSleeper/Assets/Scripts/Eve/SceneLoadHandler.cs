using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadHandler : MonoBehaviour
{
    private bool hasLoaded = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //�V�[���̐؂�ւ�蒼��ŁA��x�����s����Ă��Ȃ��Ȃ珈�������
        if (scene.name == "Game" && !hasLoaded)
        {
            hasLoaded = true;

            SaveJSON saveJSON = FindObjectOfType<SaveJSON>();
            if (saveJSON != null)
            {
                saveJSON.LoadData();
                Debug.Log("LoadData()�̓ǂݍ��ݐ���");
            }
            else
            {
                Debug.LogWarning("SaveJSON�I�u�W�F�N�g���Ȃ���");
            }


        }
    }
}
