using MapObject;
using Player;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Weapon
{
    public class EnemyBullet : Bullet
    {
        [SerializeField] private bool customSizeThreshold;
        
        [ShowIf("customSizeThreshold")]
        [SerializeField] private float sizeThreshold = 0.5f;
        
        public float damage;
        
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player 被擊中！");
                var controller = collision.gameObject.GetComponent<PlayerController>();
                
                if (!PlayerCanIgnoreDamage(controller))
                {
                    controller.TakeDamage(damage);
                }
                
                OnHit();
            }
        }
        
        private bool PlayerCanIgnoreDamage(PlayerController controller)
        {
            if (customSizeThreshold)
            {
                return controller.sizeHandler.currentSize >= sizeThreshold;
            }
            else
            {
                return controller.CanDestroyObstacle();
            }
        }
        
    }

}