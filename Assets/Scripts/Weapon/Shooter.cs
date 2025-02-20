using UnityEngine.Serialization;

namespace Weapon
{
    using UnityEngine;

    public class Shooter : MonoBehaviour
    {
        public GameObject bulletPrefab;
        
        public Transform firePoint;  
        
        
        public void Fire(int count, float spread, BulletClip clip = null)
        {
            float startAngle = -((count - 1) * spread / 2);

            for (int i = 0; i < count; i++)
            {
                float angle = startAngle + i * spread;
                Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right;

                GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
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
    }

}