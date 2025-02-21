using Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MapObject
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private float size = 0.3f;

        [Required]
        [SerializeField] private DropBulletObstacle _dropBulletObstacle;

        public bool takeDamage;
        [ShowIf("takeDamage")] 
        public float damage = 0.05f;
        
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var controller = collision.gameObject.GetComponent<PlayerController>();
                OnPlayerEnter(controller);
            }
        }


        private void OnPlayerEnter(PlayerController controller)
        {
            if (controller.sizeHandler.currentSize > size)
            {
                DestroyByPlayer();
            }
            else
            {
                if (takeDamage)
                {
                    controller.TakeDamage(damage);
                }
            }
        }

        public void DestroyByPlayer()
        {
            _dropBulletObstacle.DestroyByPlayer();
            
            Destroy(gameObject);
        }
    }
}