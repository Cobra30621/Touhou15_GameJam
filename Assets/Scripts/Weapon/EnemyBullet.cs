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
        
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("playerHitbox"))
            {
                Debug.Log("Player 被擊中！");
                var controller = PlayerController.Instance;
                
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