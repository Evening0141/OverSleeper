using UnityEngine;
using UnityEngine.UI;  // UIコンポーネントにアクセスするために必要

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;  // スライダーUIをインスペクターから設定

    // 継承先で上書きできるように virtual にする
    protected virtual string VolumeKey => "MasterVolume";

    protected virtual void ApplyVolume(float volume)
    {
        // デフォルトではAudioListenerに適用（マスターボリューム）
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
        Debug.Log("音量を読み込みました: " + savedVolume);
    }
}
