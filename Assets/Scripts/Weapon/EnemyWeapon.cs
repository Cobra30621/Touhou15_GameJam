using Player;
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

            var playerPos = PlayerController.Instance.transform.position;

            var dir = playerPos - shooter.firePoint.position;
            
            shooter.Fire(bulletCount, spreadAngle, dir);
            fireCooldown = fireRate;
        }
    }
}