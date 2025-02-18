using Player;
using UnityEngine;

namespace MapObject
{
    public class DeadZone : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                var controller = collider.gameObject.GetComponent<PlayerController>();
                controller.TakeDamage(99999999);
            }
        }
    }
}