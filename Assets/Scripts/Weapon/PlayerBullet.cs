using MapObject;

namespace Weapon
{
    using UnityEngine;

    public class PlayerBullet : Bullet
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Bullet Hit Enemy！");
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