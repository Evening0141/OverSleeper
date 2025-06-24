using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSGameManager : MonoBehaviour
{
    #region field
    // ゲームタイム
    [SerializeField] Text timerText;  // 時間表示
    private const float _GAMETIME = 45.0f;
    private float _GameTime = _GAMETIME;

    // クールタイム
    private const float _TIME = 3.0f; // 時間設定
    private float _time = _TIME;      // 時間セット
    private bool isGame = true;       // 順位確定後のクールタイムタイミング
    private bool canCheat = false;    // チート付与

    [Header("試合後表示のオブジェクト"),SerializeField] GameObject readyObj;
    [Header("ランク表示"),SerializeField] Text Rank;

    string winUser = "";  // 勝利プレイヤーの名前

    [System.Serializable]
    public class PlayerSlot
    {
        public Sprite CharaSp;                // 画像
        public GameObject spawnPoint;         // 生成場所
        public GameObject playerPrefab;       // プレイヤープレハブ
        public Image charaImg;                // キャラアイコン
        public Text nameText;                 // プレイヤー名表示

        public GameObject playerObj;          // 実体
        public Character character;           // スクリプト参照

        public bool IsDown => character != null && character.IsDown;
    }

    [SerializeField] private List<PlayerSlot> playerSlots = new List<PlayerSlot>();
    private List<PlayerSlot> activePlayerSlots = new List<PlayerSlot>();
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
        // 初期化
        winUser = "";

        // パネル非表示
        readyObj.SetActive(false);

        // 複製
        activePlayerSlots = new List<PlayerSlot>(playerSlots); 

        // 抽選
        RandCheat();

        // 時間セット
        _GameTime = _GAMETIME;

        foreach (var slot in activePlayerSlots)
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
            int index = Random.Range(0, activePlayerSlots.Count);
            activePlayerSlots[index].character.UseCheat();
        }

        // 次のスイッチ処理へ
        act = GameAct.MAIN;
    }

    private void PlayerActive()
    {
        // 現状はキャラ三体なので問題はなさそうだが
        // のちのち増えた場合処理速度で危惧すべき
        // 死亡を検知する
        for (int i = activePlayerSlots.Count - 1; i >= 0; i--)
        {
            var slot = activePlayerSlots[i];
            if (slot.IsDown)
            {
                slot.charaImg.color = Color.red;
                Destroy(slot.playerObj);
                activePlayerSlots.RemoveAt(i); // 死亡キャラをリストから削除
            }
        }
        // 時間処理
        if (_GameTime > 0)
        {
            _GameTime -= Time.deltaTime;
            if (_GameTime <= 0)
            {
                act = GameAct.RANK;
                timerText.text= "終了！";
                return;
            }
        }
        int min = Mathf.FloorToInt(_GameTime / 60);
        int sec = Mathf.FloorToInt(_GameTime % 60);
        float miri = _GameTime % 1.0f;
        // テキスト変更
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, Mathf.FloorToInt(miri * 100));

        // 生存プレイヤー数が一人又はゼロになったとき
        // ゲーム時間が終了の場合ランキングへ
        if (1>=activePlayerSlots.Count||_GameTime<=0)
        {
            if (activePlayerSlots.Count == 1)
            {
                winUser = activePlayerSlots[0].nameText.text; // 名前保管
            }
            act = GameAct.RANK;
            return;
        }
    }

    private void RankingDisp()
    {
        readyObj.SetActive(true);
        isGame = false;
        // 勝利プレイヤーを表示
        if (activePlayerSlots.Count == 1)
        {
            Rank.text = winUser + " WIN";
        }
        else　// 時間終了の場合
        {
            Rank.text = "DRAW";
        }
        for (int i = activePlayerSlots.Count - 1; i >= 0; i--)
        {
            var slot = activePlayerSlots[i];
            Destroy(slot.playerObj);
            activePlayerSlots.RemoveAt(i); // 全て削除
        }
    }

    // チート抽選
    private void RandCheat()
    {
        // データをもらう
        canCheat = DataRelay.Dr.IsCheat;

        if (canCheat)
        {
            Debug.Log("チート使用者がいます");
            return;
        }
        // 例: 90% の確率で false、10% の確率で true
        canCheat = Random.value < 0.1f;
        Debug.Log(canCheat + "チートの有無");

        // 抽選後に中継に情報を送る
        DataRelay.Dr.IsCheat = canCheat;
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
