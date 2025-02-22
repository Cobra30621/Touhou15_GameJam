using MapObject;
using Reimu;

namespace Weapon
{
    using UnityEngine;

    public class SuikaBullet : Bullet
    {
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Reimu"))
            {
                Debug.Log("Bullet Hit Reimu¡I");
                var hitTrigger = collision.gameObject.GetComponent<ReimuHitTrigger>();
                hitTrigger.OnHit();
                OnHit();
            }

            if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                Debug.Log("Bullet Hit Obstacle¡I");
                var obstacle = collision.gameObject.GetComponent<Obstacle>();

                if (obstacle != null)
                {
                    obstacle.DestroyByPlayer();
                }
                OnHit();
            }
        }

    }

}