namespace Weapon
{
    using UnityEngine;

    public class PlayerBullet : Bullet
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                Debug.Log("Enemy 被擊中！");
                Destroy(gameObject);
            }
        }
    }

}