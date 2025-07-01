using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffObj;

    public float lifeTime = 3f;
    public int damage = 10;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 敵以外に命中したら即削除
        if (!other.CompareTag("Character"))
        {
            Destroy(gameObject);
        }
        // 生成
        GameObject hitObj;
        // 当たったオブジェクト座標
        Vector3 hitPos = other.transform.position;
        // オフセット
        float ofsY = 4.0f;
        hitPos.y = ofsY;

        // CharacterBase を取得してダメージを与える
        CharacterBase target = other.GetComponent<CharacterBase>();
        if (target != null)
        {
            target.TakeDamage(damage);
            // エフェクト生成
            hitObj = Instantiate(hitEffObj, hitPos, Quaternion.identity);
            Destroy(hitObj, lifeTime);
        }
        Debug.Log("HIT");
        Destroy(gameObject);
    }
}
