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

        /// <summary>   
        /// when player takes damage -> decrease size
        /// note : size limit is 0~1 
        /// <!summary>
        public void TakeDamage(float damage)
        {
            sizeHandler.Shrink(damage);
        }

        private void Die()
        {
            // 處理死亡邏輯
        }
    }
}