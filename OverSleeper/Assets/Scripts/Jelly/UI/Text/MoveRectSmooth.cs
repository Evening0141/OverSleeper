using UnityEngine;

public class MoveRectSmooth : MonoBehaviour
{
    [Header("移動設定")]
    public Vector2 direction = Vector2.left;  // 移動方向
    public float speed = 150f;                // 移動速度（px/sec）

    const float _DELETEPOS = -2000.0f;

    private RectTransform rectTransform;

    // 初期化：自身のRectTransformを取得
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // フレームごとの移動処理
    void Update()
    {
        // 移動方向に基づいて位置を更新
        rectTransform.anchoredPosition += direction.normalized * speed * Time.deltaTime;

        // 削除条件：自身の位置が「削除基準オブジェクト」より左に行ったら
        if ( rectTransform.anchoredPosition.x < _DELETEPOS)
        {
            Destroy(gameObject); // このオブジェクトを削除
        }
    }
}
