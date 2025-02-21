using Player;
using UnityEngine;
namespace Weapon
{
    public class EnemyBullet : Bullet
    {
        public float damage;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Player 被擊中！");
                var controller = collision.gameObject.GetComponent<PlayerController>();
                controller.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

}