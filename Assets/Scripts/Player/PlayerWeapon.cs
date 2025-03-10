﻿using System.Collections.Generic;
using UnityEngine;
using Weapon;

namespace Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private float fireCooldown = 0f;

        [SerializeField] public Shooter shooter;
        [SerializeField] private float bulletSpeed = 5f;

        [SerializeField] public int damage = 1;

        public KeyCode shootKey;
        public int bulletCount = 1;  
        public float fireRate = 0.5f; 
        public float spreadAngle = 15f;

        [SerializeField] public List<BulletClip> bulletClips = new List<BulletClip>();
        
        
        void Update()
        {
            fireCooldown -= Time.deltaTime;
            if (Input.GetKeyDown(shootKey) && fireCooldown <= 0f && bulletClips.Count > 0)
            {
                Fire();
            }
        }


        private void Fire()
        {
            Debug.Log("Fire");
            var bulletSprite = bulletClips[0];
            bulletClips.RemoveAt(0);
            
            PlayerController.Instance.OnBulletClipChanged?.Invoke(bulletClips);
            var dir = PlayerController.Instance.LeftDirection ? new Vector2(-1, 0) : new Vector2(1, 0);
            
            shooter.Fire(bulletCount, spreadAngle, dir,bulletSpeed,false ,bulletSprite);
            fireCooldown = fireRate;
        }
        
        
        public void AddBullet(BulletClip clip)
        {
            Debug.Log("Addbullet");
            bulletClips.Add(clip);
        }
    }
}