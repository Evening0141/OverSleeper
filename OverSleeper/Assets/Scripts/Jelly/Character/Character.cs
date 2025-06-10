public class Character : CharacterBase
{
    /// <summary>
    /// ステータスのセット
    /// </summary>
    public void PlayerStatus()
    {
        CharaSetUp();
    }

    public void UseCheat()
    {
        // チート使用の能力アップ
        hp = 200;
        hit = 1.0f;
        moveSpd = 25.0f;
        shotCooldown = 0.5f;
    }
    /// <summary>
    /// プレイヤーのメイン処理
    /// </summary>
    private void Update()
    {
        CharaUpdate();
    }
}
