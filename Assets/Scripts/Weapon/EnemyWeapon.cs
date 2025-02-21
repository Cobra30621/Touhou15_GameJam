using Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Weapon
{
    /// <summary>
    /// Represents the weapon used by enemies.
    /// </summary>
    public class EnemyWeapon : MonoBehaviour
    {
        /// <summary>
        /// The cooldown time before the weapon can fire again.
        /// </summary>
        [SerializeField] private float fireCooldown = 0f;

        /// <summary>
        /// The shooter component responsible for firing bullets.
        /// </summary>
        [SerializeField] private Shooter shooter;

        /// <summary>
        /// The number of bullets to fire in a single shot.
        /// </summary>
        public int bulletCount = 3;  

        /// <summary>
        /// The rate at which the weapon can fire (in seconds).
        /// </summary>
        public float fireRate = 0.5f; 

        /// <summary>
        /// The angle of spread for the bullets when fired.
        /// </summary>
        public float spreadAngle = 15f;

        /// <summary>
        /// Indicates whether to use the player's direction.
        /// </summary>
        public bool usePlayerDirection = true;

        /// <summary>
        /// The custom direction to fire towards.
        /// </summary>
        [ShowIf("@!usePlayerDirection")]
        public Vector3 customDirection;

        /// <summary>
        /// Updates the weapon's state every frame, checking if it can fire.
        /// </summary>
        void Update()
        {
            fireCooldown -= Time.deltaTime;
            if (fireCooldown <= 0f)
            {
                Fire();
            }
        }

        /// <summary>
        /// Fires the weapon, shooting bullets towards the player.
        /// </summary>
        private void Fire()
        {
            Debug.Log("Fire");

            // 獲取玩家位置
            var playerPos = PlayerController.Instance.transform.position;

            // 定義發射方向
            Vector3 dir;

            // 根據需要選擇發射方向
            if (usePlayerDirection) // 假設有一個布林變數用來決定是否使用玩家方向
            {
                dir = playerPos - shooter.firePoint.position;
            }
            else
            {
                dir = customDirection; // 假設有一個 Vector3 變數 customDirection 用來定義自定義方向
            }
            
            shooter.Fire(bulletCount, spreadAngle, dir);
            fireCooldown = fireRate;
        }
    }
}