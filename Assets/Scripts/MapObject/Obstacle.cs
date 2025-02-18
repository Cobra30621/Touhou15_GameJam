using Player;
using UnityEngine;

namespace MapObject
{
    public class Obstacle : MonoBehaviour
    {
        public int damage = 1;

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var controller = collision.gameObject.GetComponent<PlayerController>();
                controller.TakeDamage(damage);
            }
        }
    }
}