using Player;
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


        private bool isShooting;

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
                    bulletWave++;
                    if (bulletWave == currentWave)
                    {
                        bulletWave 
                        isShooting = false;
                    }
                    waveCooldown = waveRate;
                }

            } else
            {
                fireCooldown -= Time.deltaTime;
                if (fireCooldown <= 0f) { 
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
            Debug.Log("Fire");

            var playerPos = PlayerController.Instance.transform.position;

            var dir = playerPos - shooter.firePoint.position;
            
            shooter.Fire(bulletCount, spreadAngle, dir);
            //fireCooldown = fireRate;
        }
    }
}