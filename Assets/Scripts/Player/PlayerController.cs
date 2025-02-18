using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerSizeHandler sizeHandler;
        private PlayerMovement playerMovement;

        void Start()
        {
            sizeHandler = GetComponent<PlayerSizeHandler>();
            playerMovement = GetComponent<PlayerMovement>();
        }
        
        public void Hurt()
        {
            
        }

        private void Die()
        {
            // 處理死亡邏輯
        }
    }
}