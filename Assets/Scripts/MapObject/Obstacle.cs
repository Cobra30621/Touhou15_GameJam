using Player;
using UnityEngine;

namespace MapObject
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private float size = 0.3f;

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var controller = collision.gameObject.GetComponent<PlayerController>();
                if (controller.sizeHandler.currentSize > size)
                {
                    DestroyThis();
                }
            }
        }
        
        private void DestroyThis()
        {
            Destroy(gameObject);
        }
    }
}