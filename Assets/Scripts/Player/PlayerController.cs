using System;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Weapon;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

        [Header("Reference")]
        [SerializeField] public PlayerSizeHandler sizeHandler;
        [SerializeField] public PlayerMovement playerMovement;
        [SerializeField] public PlayerWeapon playerWeapon;
        [SerializeField] public SpriteRenderer spriteRenderer;
        [SerializeField] public Animator _animator;

        [Header("Setting")]
        [SerializeField] private float invincibleCooldown = 1f;
        [SerializeField] private float idleThershold = 0.05f;
        [SerializeField] private float destroyObstacleSize = 0.6f;

        [Header("Status")]
        [SerializeField] public bool isInvincible = false;
        [SerializeField] public bool isDead;


        public KeyCode item1Key = KeyCode.X;
        public KeyCode item2Key = KeyCode.C;


        public UnityEvent<List<BulletClip>> OnBulletClipChanged = new UnityEvent<List<BulletClip>>();

        public bool LeftDirection => playerMovement.leftDirection;

        
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
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _animator = GetComponent<Animator>();   
        }

        private void Update()
        {
            CheckImmortal();
            UpdateAnimator();

            //使用主動道具
            if (Input.GetKeyDown(item1Key))
            {
                ItemManager.Instance.useActivateItem(0);
            }

            if (Input.GetKeyDown(item2Key))
            {
                ItemManager.Instance.useActivateItem(1);
            }
        }
        
        private void CheckImmortal()
        {
            if (isInvincible) {
                spriteRenderer.enabled = !spriteRenderer.enabled;
            } else if (!spriteRenderer.enabled) {
                spriteRenderer.enabled = true;
            }
        }

        private void UpdateAnimator()
        {
            if (isDead)
            {
                _animator.SetTrigger("Die");
                return;
            }


            if (playerMovement.moveInput != 0)
            {
                _animator.SetBool("Walking", true);
            }
            else if (playerMovement.idleTimer > idleThershold)
            {
                _animator.SetBool("Walking", false);
            }

        }
        private IEnumerator SetInvincible()
        {
            isInvincible = true;
            yield return new WaitForSeconds(invincibleCooldown);
            isInvincible = false;
        }

        public void TakeDamage(float damage)
        {
            if (isInvincible) { return; }

            sizeHandler.Resize(-damage);
            StartCoroutine(SetInvincible());
        }

        [Button]
        public void Die()
        {
            isDead = true;
            playerMovement.Freeze();
            sizeHandler.enabled = false;
        }
        
        public void AddBullet(BulletClip clip)
        {
            playerWeapon.AddBullet(clip);
            OnBulletClipChanged?.Invoke(playerWeapon.bulletClips);
        }

        public bool CanDestroyObstacle()
        {
            return sizeHandler.currentSize >= destroyObstacleSize;
        }
    }
}