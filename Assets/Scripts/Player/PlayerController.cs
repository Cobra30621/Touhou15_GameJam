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
        
        public void TakeDamage(int damage)
        {
            Debug.Log($"Take Damage {damage}");
        }

        private void Die()
        {
            // 處理死亡邏輯
        }
    }
}