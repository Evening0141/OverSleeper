using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected int hp;     　　  // プレイヤーHP
    protected string nameId;  // プレイヤー名
    protected float hit;  　　// 射撃精度
    protected float moveSpd;   // 移動速度
    protected float resSpd;    // リスポーンクールタイム

    // 移動処理
    public virtual void Move() {}

    // 射撃処理
    public virtual void Shot() {}

    // リスポーン処理
    public virtual void Respwan() {}

    // 敵検知
    public virtual bool EnemySearch() { return false; }

    // 壁検知
    public virtual bool WallSearch() { return false; }

    // チート
    public virtual void UpStatus() {}

    // ダメージを受ける処理
    public virtual void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"{nameId} が {damage} ダメージを受けた！ 残りHP: {hp}");

        if (hp <= 0)
        {
            Die();
        }
    }

    // 死亡処理
    protected virtual void Die()
    {
        Debug.Log($"{nameId} は倒れた");
        // ここに死亡時の処理（アニメーションやリスポーン）を記述
        gameObject.SetActive(false);
    }
}