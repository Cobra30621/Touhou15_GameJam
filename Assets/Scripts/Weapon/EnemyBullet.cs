using Player;

namespace Weapon
{
    using UnityEngine;

    public class EnemyBullet : Bullet
    {
        public float damage;
        
        protected override void Update()
        {
            base.Update();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Player 被擊中！");
                var controller = collision.gameObject.GetComponent<PlayerController>();
                controller.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

}