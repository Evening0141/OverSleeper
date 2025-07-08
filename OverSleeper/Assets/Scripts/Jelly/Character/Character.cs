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

        // メッセージを送信
        SendDiscordMessage("私がチーターです。");
    }

    public override void Die()
    {
        // この個体がなくなるまで同じ名前は存在させない
        NameGenerator.ReleaseName(nameId); // 名前の開放
        isDown = true; // 死亡
        // メッセージを送信
        SendDiscordMessage(nameId + "は倒れた");
        //gameObject.SetActive(false);
    }

    /// <summary>
    /// プレイヤーのメイン処理
    /// </summary>
    private void Update()
    {
        CharaUpdate();
    }
}
