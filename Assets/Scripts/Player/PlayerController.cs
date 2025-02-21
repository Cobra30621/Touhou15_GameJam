using System;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using Weapon;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;
        
        public PlayerSizeHandler sizeHandler;
        private PlayerMovement playerMovement;

        private PlayerWeapon playerWeapon;
        
        [SerializeField] private SpriteRenderer spriteRenderer;

        
        private float immortalCooldown = 1f;
        public bool isImmortal = false;
        public bool isDead;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        void Start()
        {
            sizeHandler = GetComponent<PlayerSizeHandler>();
            playerMovement = GetComponent<PlayerMovement>();
            playerWeapon = GetComponent<PlayerWeapon>();
        }

        private void Update()
        {
            if (isImmortal) {
                spriteRenderer.enabled = !spriteRenderer.enabled;
            } else if (!spriteRenderer.enabled)
            {
                spriteRenderer.enabled = true;
            }
            //need to be modify later
            detect_die();
        }
        
        
        private void detect_die()
        {
            if (sizeHandler.currentSize <= 0)
            {
                Die();
            }
        }
        
        
        
        /// <summary>   
        /// when player takes damage -> decrease size
        /// note : size limit is 0~1 
        /// <!summary>
        public void TakeDamage(float damage)
        {
            if(isImmortal){return;}
            
            sizeHandler.Resize(-damage);
            StartCoroutine(Immortal());
        }

        public void Die()
        {
            print("Player Die");
            isDead = true;
        }

        public void AddBullet(BulletClip clip)
        {
            playerWeapon.AddBullet(clip);
        }

    
        private IEnumerator Immortal()
        {
            isImmortal = true;
            yield return new WaitForSeconds(immortalCooldown);
            isImmortal = false;
        }

        public void Resize(float amount)
        {
            sizeHandler.Resize(0.1f);
        }
    }
}