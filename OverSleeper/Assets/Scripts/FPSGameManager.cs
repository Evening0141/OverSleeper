using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSGameManager : MonoBehaviour
{
    private const float _TIME = 3.0f; // 時間設定
    private float _time = _TIME;
    private bool isGame = true; // 順位確定後のクールタイムタイミング
    private bool canCheat = false; // チート付与
    
    [SerializeField] GameObject[] spawnObj;  // 生成場所
    [SerializeField] GameObject[] playerObj; // プレイヤー
    // Characterスクリプトのメイン処理は
    // プレイヤーオブジェクト自体が各自のアップデートで処理
    Character[] characterScr;                // キャラクターのスクリプト（配列の初期化は使用する際にメソッドで行う）
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
        if (!isGame) {
            EndCoolTime();
            return; 
        }
        else if(isGame)
        {
            switch(act)
            {
                case GameAct.SPAWN:
                    PlayerSet();
                    break;
                case GameAct.MAIN:
                    PlayerActive();
                    break;
                case GameAct.RANK:
                    break;
            }
        }
    }

    // 生成処理
    private void PlayerSet()
    {
        // 配列の初期化
        characterScr = new Character[playerObj.Length];
        // 抽選
        RandCheat();
        // 生成ループ
        for(int i=0;i<playerObj.Length;i++)
        {
            GameObject obj = Instantiate(playerObj[i], spawnObj[i].transform.position, Quaternion.identity);
            characterScr[i] = obj.GetComponent<Character>();
            // 生成キャラの初期化を行う
            characterScr[i].PlayerStatus();
        }
        // trueを引いた場合は誰かに付与
        if (canCheat) {
            // ランダムにプレイヤーを選択
            int index = Random.Range(0, characterScr.Length);
            // チート付与
            characterScr[index].UseCheat();
        }
        // 次のスイッチ処理へ
        act = GameAct.MAIN;
    }

    // 死亡を検知する
    private void PlayerActive()
    {
        
    }

    // チート抽選
    private void RandCheat()
    {
        if (canCheat) {Debug.Log("チート使用者がいます"); return; }
        // 例: 90% の確率で false、10% の確率で true
        canCheat = Random.value < 0.1f;
        Debug.Log(canCheat+"デバッグだよ");
    }

    // 待機計算
    private void EndCoolTime()
    {
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            isGame = true; // 準備完了
            _time = _TIME; // 再セット
        }
    }
}
