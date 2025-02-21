namespace MapObject
{
    using UnityEngine;

    public class BouncePlatform : MonoBehaviour
    {
        public float bounceForce = 10f; // 彈跳力度
        
        
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player")) // 確保是玩家踩到
            {
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rb != null && rb.velocity.y <= 0) // 只在玩家向下落時觸發
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0); // 清除原本 Y 軸速度
                    rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
                }
            }
        }
    }

}