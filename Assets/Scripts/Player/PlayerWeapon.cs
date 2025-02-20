using System.Collections.Generic;
using UnityEngine;
using Weapon;

namespace Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private float fireCooldown = 0f;

        [SerializeField] private Shooter shooter;

        public int bulletCount = 1;  
        public float fireRate = 0.5f; 
        public float spreadAngle = 15f;

        private List<BulletClip> bulletClips = new List<BulletClip>();
        
        
        void Update()
        {
            fireCooldown -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space) && fireCooldown <= 0f && bulletClips.Count > 0)
            {
                Fire();
            }
        }


        private void Fire()
        {
            var bulletSprite = bulletClips[0];
            bulletClips.RemoveAt(0);
            
            shooter.Fire(bulletCount, spreadAngle, bulletSprite);
            fireCooldown = fireRate;
        }
        
        
        public void AddBullet(BulletClip clip)
        {
            bulletClips.Add(clip);
        }
    }
}