
[System.Serializable]//これがないとSoundManagerのinspectorに表示されないため
public class SoundGroup
{
    public string groupname; //BGM or SEかを判断するための変数
    public Sound[] sounds; //それぞれのSound関連の配列
}
