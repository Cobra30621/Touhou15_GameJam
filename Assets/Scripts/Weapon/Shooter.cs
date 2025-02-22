using Sirenix.OdinInspector;
using UnityEngine.Serialization;

namespace Weapon
{
    using UnityEngine;

    /// <summary>
    /// This class is responsible for shooting bullets from a specified fire point.
    /// </summary>
    public class Shooter : MonoBehaviour
    {
        /// <summary>
        /// The prefab for the bullet that will be instantiated when shooting.
        /// </summary>
        [Required]
        public GameObject bulletPrefab;

        /// <summary>
        /// The point from which the bullets will be fired.
        /// </summary>
        [Required]
        public Transform firePoint;  
        
        /// <summary>
        /// Fires a specified number of bullets in a given direction with a spread.
        /// </summary>
        /// <param name="count">The number of bullets to fire.</param>
        /// <param name="spread">The angle spread between bullets.</param>
        /// <param name="direction">The direction in which to fire the bullets.</param>
        /// <param name="clip">Optional bullet clip for additional properties.</param>
        public void Fire(int count, float spread, Vector2 direction, float speed = 5,BulletClip clip = null)
        {
            float startAngle = -((count - 1) * spread / 2);

            for (int i = 0; i < count; i++)
            {
                float angle = startAngle + i * spread;
                Vector2 dir = Quaternion.Euler(0, 0, angle) * direction;

                GameObject bulletObj = Instantiate(bulletPrefab,  GetFirePoint(), Quaternion.identity);
                Bullet bullet = bulletObj.GetComponent<Bullet>();

                if (clip != null)
                {
                    bullet.Initialize(dir, clip.Sprite);
                }
                else
                {
                    bullet.Initialize(dir);
                }
            }
        }

        /// <summary>
        /// Draws gizmos in the editor to visualize the fire point.
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(GetFirePoint(), 0.1f); // 在編輯器中繪製 firePoint 的位置
        }
        
        /// <summary>
        /// Gets the position of the fire point.
        /// </summary>
        /// <returns>The position of the fire point as a Vector3.</returns>
        private Vector3 GetFirePoint()
        {
            return firePoint.position;
        }

    }

}