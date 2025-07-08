using UnityEngine;

public class AudioManager
{
    private BGMPlaylist bgmPlayL;       // �Đ�����BGM
    private SEPlaylist sePlayL;       // �Đ�����BGM
    private GameObject _bgm;            // �Đ�����I�u�W�F�N�g
    private GameObject _se;             // �Đ�����I�u�W�F�N�g
    private AudioSource audioSource_bgm;// �R���|�[�l���g�擾�p
    private AudioSource audioSource_se; // �R���|�[�l���g�擾�p

    string SoundName_BGM;�@// DataRelay����̎󂯎��
    string SoundName_SE;   // DataRelay����̎󂯎��

    private DataRelay dr;               // �Đ�����T�E���h�����擾

    private int BGMListNum = 0;

    // �R���X�g���N�^
    public AudioManager()
    {
        // �ݒ�
        dr = DataRelay.Dr;

        // ���\�[�X����Ăяo��
        bgmPlayL = Resources.Load<BGMPlaylist>("BGMPlaylist");
        sePlayL = Resources.Load<SEPlaylist>("SEPlaylist");
        GameObject prefab = Resources.Load<GameObject>("SOUND");

        // �I�u�W�F�N�g�𐶐������ăR���|�[�l���g�擾
        _bgm = Object.Instantiate(prefab);
        _se = Object.Instantiate(prefab);
        audioSource_bgm = _bgm.GetComponent<AudioSource>();
        audioSource_se = _se.GetComponent<AudioSource>();
    }

    /// <summary>
    /// ���ʂ������ŕύX����̂ŃA�b�v�f�[�g�K�{����
    /// </summary>
    public void VolumeSet()
    {
        audioSource_bgm.volume=PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        audioSource_se.volume=PlayerPrefs.GetFloat("SEVolume", 0.5f);
    }

    public void Play_BGM()
    {
        SoundName_BGM = dr.Data_BGM.ToString();

        if (!audioSource_bgm.isPlaying)
        {
            for (int i = 0; i < bgmPlayL.bgmClips.Count; i++)
            {
                // ��v�������̂��Đ�����
                if (SoundName_BGM == bgmPlayL.bgmClips[i].name)
                {
                    audioSource_bgm.clip = bgmPlayL.bgmClips[i];
                    audioSource_bgm.Play();
                    dr.Data_BGM = DataRelay.BGM_Name.None;

                    // �I�Ȕԍ��������Ń}�b�N�X�ɂȂ�����O
                    if (i == bgmPlayL.bgmClips.Count - 1)
                    {
                        BGMListNum = 0;
                    }
                    else
                    {
                        BGMListNum = i;
                    }
                    Debug.Log(i);
                }
                else if (SoundName_BGM == "None")
                {
                    audioSource_bgm.clip = bgmPlayL.bgmClips[BGMListNum + 1];
                    audioSource_bgm.Play();
                }
            }
        }
    }

    public void Play_SE()
    {
        SoundName_SE = dr.Data_SE.ToString();

        if (SoundName_SE == "None") { return; }
        for (int i = 0; i < sePlayL.seClips.Count; i++)
        {
            // ��v�������̂��Đ�����
            if (SoundName_SE == sePlayL.seClips[i].name)
            {
                audioSource_se.clip = sePlayL.seClips[i];
                audioSource_se.Play();
                dr.Data_SE = DataRelay.SE_Name.None;
            }
            Debug.Log("SE"+SoundName_SE);
        }
    }
}
