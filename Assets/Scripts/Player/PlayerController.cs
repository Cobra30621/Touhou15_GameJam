using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerSizeHandler sizeHandler;
        private PlayerMovement playerMovement;
        private float immortalCooldown = 1f;
        public bool isImmortal = false;
        [SerializeField] private SpriteRenderer spriteRenderer;

        void Start()
        {
            sizeHandler = GetComponent<PlayerSizeHandler>();
            playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            if (isImmortal) {
                spriteRenderer.enabled = !spriteRenderer.enabled;
            } else if (!spriteRenderer.enabled)
            {
                spriteRenderer.enabled = true;
            }
            //need to be modify later
            //detect_die();
        }
        //private void detect_die()
        //{
        //    if (sizeHandler.currentSize <= 0)
        //    {
        //        Die();
        //    }
        //}
        /// <summary>   
        /// when player takes damage -> decrease size
        /// note : size limit is 0~1 
        /// <!summary>
        public void TakeDamage(float damage)
        {
            sizeHandler.Resize(-damage);
            StartCoroutine(Immortal());
        }

        public void Die()
        {
            playerMovement.Freeze();
            playerMovement.enabled = false;
            sizeHandler.enabled = false;
        }

        public bool IsImmortal()
        {
            return isImmortal;
        }
        private IEnumerator Immortal()
        {
            isImmortal = true;
            yield return new WaitForSeconds(immortalCooldown);
            isImmortal = false;
        }

        public void Resize(float amount)
        {
            sizeHandler.Resize(0.1f);
        }

        public float getVelocityX()
        {
            return playerMovement.GetVelocityX();
        }
    }
}