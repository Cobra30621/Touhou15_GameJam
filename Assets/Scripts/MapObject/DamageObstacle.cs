using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapObject
{
    public class DamageObstacle : MonoBehaviour
    {
        public float damage = 0.05f;
        private float damageCooldown = 1f;
        private bool iscooldown = false;
        [SerializeField] private float size = 0.3f;

        private void Start()
        {
            iscooldown = false;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var controller = collision.gameObject.GetComponent<PlayerController>();
                if (controller.sizeHandler.currentSize > size)
                {
                    DestroyThis();
                }
                else if (!controller.IsImmortal())
                {
                    controller.TakeDamage(damage);
                    print("Player take damage");
                    //StartCoroutine(DamageCooldown());
                }
            }
        }
        private IEnumerator DamageCooldown()
        {
            iscooldown = true;
            yield return new WaitForSeconds(damageCooldown);
            iscooldown = false;
        }
        private void DestroyThis()
        {
            Destroy(gameObject);
        }
    }
}