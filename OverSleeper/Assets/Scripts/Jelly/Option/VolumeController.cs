using UnityEngine;
using UnityEngine.UI;  // UI�R���|�[�l���g�ɃA�N�Z�X���邽�߂ɕK�v

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;  // �X���C�_�[UI���C���X�y�N�^�[����ݒ�

    // �p����ŏ㏑���ł���悤�� virtual �ɂ���
    protected virtual string VolumeKey => "MasterVolume";

    protected virtual void ApplyVolume(float volume)
    {
        // �f�t�H���g�ł�AudioListener�ɓK�p�i�}�X�^�[�{�����[���j
        AudioListener.volume = volume;
    }

    public void SAVE()
    {
        float volume = volumeSlider.value;
        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
        ApplyVolume(volume);
    }

    public void LOAD()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 0.5f);
        volumeSlider.value = savedVolume;
        ApplyVolume(savedVolume);
        Debug.Log("���ʂ�ǂݍ��݂܂���: " + savedVolume);
    }
}
