using Player;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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

        public int bulletWave = 3;

        [SerializeField] private int currentWave = 0;

        [SerializeField] private float waveCooldown = 0f;

        public float waveRate = 0.5f;

        public float bulletSpeed = 5f;

        public bool need_rotate = false;

        private bool isShooting;

        /// <summary>
        /// Indicates whether to use the player's direction.
        /// </summary>
        public bool useTargetPosition = true;

        /// <summary>
        /// The custom direction to fire towards.
        /// </summary>
        [ShowIf("@!useTargetPosition")]
        public Vector3 customDirection;

        [ShowIf("@useTargetPosition")]
        public Vector3 targetDirection;

        /// <summary>
        /// Updates the weapon's state every frame, checking if it can fire.
        /// </summary>
        void Update()
        {
            if (isShooting)
            {
                waveCooldown -= Time.deltaTime;
                if (waveCooldown <= 0f)
                {
                    Fire();
                    currentWave++;
                    if (bulletWave == currentWave)
                    {
                        currentWave = 0;
                        isShooting = false;
                        fireCooldown = fireRate;
                        return;
                    }
                    waveCooldown = waveRate;
                }

            } else
            {
                fireCooldown -= Time.deltaTime;
                if (fireCooldown <= 0f) {
                    var targetPosition = PlayerController.Instance.transform.position;
                    targetDirection = targetPosition - shooter.firePoint.position;
                    isShooting = true;
                    waveCooldown = waveRate;
                    currentWave = 0;
                }
            }
        }

        /// <summary>
        /// Fires the weapon, shooting bullets towards the player.
        /// </summary>
        private void Fire()
        {
           // Debug.Log("Fire");

            // 獲取玩家位置

            // 定義發射方向
            Vector3 dir;

            // 根據需要選擇發射方向
            if (useTargetPosition) // 假設有一個布林變數用來決定是否使用玩家方向
            {
                dir = targetDirection;
            }
            else
            {
                dir = customDirection; // 假設有一個 Vector3 變數 customDirection 用來定義自定義方向
            }
            
            shooter.Fire(bulletCount, spreadAngle, dir, bulletSpeed,need_rotate);
            //fireCooldown = fireRate;
        }

    }
}