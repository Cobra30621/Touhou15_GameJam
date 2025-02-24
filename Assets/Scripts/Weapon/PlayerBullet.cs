using MapObject;
using Reimu;

namespace Weapon
{
    using Player;
    using UnityEngine;

    public class PlayerBullet : Bullet
    {
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Bullet Hit Enemy！");
                OnHit();
            }
            
            if (collision.gameObject.CompareTag("Reimu"))
            {
                Debug.Log("Bullet Hit Reimu！");
                var hitTrigger = collision.gameObject.GetComponent<ReimuHitTrigger>();
                hitTrigger.OnHit(PlayerController.Instance.playerWeapon.damage);
                OnHit();
            }

            if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                Debug.Log("Bullet Hit Obstacle！");
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