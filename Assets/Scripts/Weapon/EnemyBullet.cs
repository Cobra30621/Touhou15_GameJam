using Player;
using UnityEngine;
namespace Weapon
{
    public class EnemyBullet : Bullet
    {
        public float damage;
        
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player 被擊中！");
                var controller = collision.gameObject.GetComponent<PlayerController>();
                controller.TakeDamage(damage);
                
                OnHit();
            }
            
            if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                Debug.Log("Bullet Hit Obstacle！");
                
                OnHit();
            }
        }
        
    }

}