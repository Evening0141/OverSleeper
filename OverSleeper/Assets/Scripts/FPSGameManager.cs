using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSGameManager : MonoBehaviour
{
    #region field
    private const float _TIME = 3.0f; // 時間設定
    private float _time = _TIME;      // 時間セット
    private bool isGame = true;       // 順位確定後のクールタイムタイミング
    private bool canCheat = false;    // チート付与

    [System.Serializable]
    public class PlayerSlot
    {
        public GameObject spawnPoint;         // 生成場所
        public GameObject playerPrefab;       // プレイヤープレハブ
        public Image charaImg;                // キャラアイコン
        public Text nameText;                 // プレイヤー名表示

        public GameObject playerObj;          // 実体
        public Character character;           // スクリプト参照

        public bool IsDown => character != null && character.IsDown;
    }

    [SerializeField] private List<PlayerSlot> playerSlots = new List<PlayerSlot>();
    #endregion

    // スイッチ処理の判断
    private enum GameAct
    {
        SPAWN, // プレイヤーの生成
        MAIN,  // ゲーム開始
        RANK,  // 順位表示
    }

    // 初期はプレイヤーのスポーンから
    private GameAct act = GameAct.SPAWN;

    private void Update()
    {
        FPSUpdate();
    }

    // メイン処理（メインマネージャーでの呼び出し）
    public void FPSUpdate()
    {
        // クールタイム中ならこのまま動かさず待機
        if (!isGame)
        {
            EndCoolTime();
            return;
        }
        else if (isGame)
        {
            switch (act)
            {
                case GameAct.SPAWN: // プレイヤー生成や初期化
                    PlayerSet();
                    break;
                case GameAct.MAIN:  // 生存中かどうか
                    PlayerActive();
                    break;
                case GameAct.RANK:  // ランキングに表示
                    RankingDisp();
                    break;
            }
        }
    }

    // 生成処理
    private void PlayerSet()
    {
        RandCheat();

        foreach (var slot in playerSlots)
        {
            GameObject obj = Instantiate(slot.playerPrefab, slot.spawnPoint.transform.position, Quaternion.identity);
            Character chara = obj.GetComponent<Character>();
            chara.PlayerStatus(); // 初期化

            slot.playerObj = obj;
            slot.character = chara;
            slot.nameText.text = chara.GetUserID;
            slot.charaImg.color = Color.white; // 死亡キャラだと赤に変更されているので
        }

        // trueを引いた場合は誰かに付与
        if (canCheat)
        {
            int index = Random.Range(0, playerSlots.Count);
            playerSlots[index].character.UseCheat();
        }

        // 次のスイッチ処理へ
        act = GameAct.MAIN;
    }

    // 現状はキャラ三体なので問題はなさそうだが
    // のちのち増えた場合処理速度で危惧すべき
    // 死亡を検知する
    private void PlayerActive()
    {
        for (int i = playerSlots.Count - 1; i >= 0; i--)
        {
            var slot = playerSlots[i];
            if (slot.IsDown)
            {
                slot.charaImg.color = Color.red;
                slot.playerObj.transform.position = new Vector3(0, -255, 0);
                playerSlots.RemoveAt(i); // 死亡キャラをリストから削除
            }
        }

        // 生存プレイヤー数が一人又はゼロになったときランキングへ
        if (1>=playerSlots.Count)
        {
            act = GameAct.RANK;
        }
    }

    private void RankingDisp()
    {

    }

    // チート抽選
    private void RandCheat()
    {
        if (canCheat)
        {
            Debug.Log("チート使用者がいます");
            return;
        }
        // 例: 90% の確率で false、10% の確率で true
        canCheat = Random.value < 0.1f;
        Debug.Log(canCheat + "チートの有無");
    }

    // 待機計算
    private void EndCoolTime()
    {
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            act = GameAct.SPAWN; // 次のバトル準備
            isGame = true; // 準備完了
            _time = _TIME; // 再セット
        }
    }
}
