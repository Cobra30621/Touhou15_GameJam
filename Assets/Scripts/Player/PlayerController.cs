using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerSizeHandler sizeHandler;
        private PlayerMovement playerMovement;

        void Start()
        {
            sizeHandler = GetComponent<PlayerSizeHandler>();
            playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            //need to be modify later
            detect_die();
        }
        private void detect_die()
        {
            if (sizeHandler.currentSize <= 0)
            {
                Die();
            }
        }
        /// <summary>   
        /// when player takes damage -> decrease size
        /// note : size limit is 0~1 
        /// <!summary>
        public void TakeDamage(float damage)
        {
            //sizeHandler.Shrink(damage);
        }

        private void Die()
        {
            //print("Player Die");
        }

        public void Resize(float amount)
        {
            sizeHandler.Resize(0.1f);
        }
    }
}