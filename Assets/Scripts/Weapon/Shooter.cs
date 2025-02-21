using Sirenix.OdinInspector;
using UnityEngine.Serialization;

namespace Weapon
{
    using UnityEngine;

    public class Shooter : MonoBehaviour
    {
        [Required]
        public GameObject bulletPrefab;

        [Required]
        public Transform firePoint;  
        
        public void Fire(int count, float spread, Vector2 direction, BulletClip clip = null)
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

        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(GetFirePoint(), 0.1f); // 在編輯器中繪製 firePoint 的位置
        }
        
        private Vector3 GetFirePoint()
        {
            return firePoint.position;
        }

    }

}