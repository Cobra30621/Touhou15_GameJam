using System;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Weapon;
using Core;
using Feedback;

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
        public Transform UsingItemDisplayPos;
        [Required]
        [SerializeField] private ParticleFeedback deadFeedback;
        [Required]
        [SerializeField] private ParticleFeedback explosionFeedback;
        [Required]
        [SerializeField] private GameObject allFeedbacks;


        
        
        [Header("Input")]
        [SerializeField] private KeyCode moveLeftKey = KeyCode.A;
        [SerializeField] private KeyCode moveRightKey = KeyCode.D;
        [SerializeField] private KeyCode jumpKey = KeyCode.W;
        
        
        [Header("Setting")]
        [SerializeField] private const float invincibleCooldown = 1f;
        [SerializeField] private float idleThershold = 0.05f;
        

        [Header("Status")]
        [SerializeField] public bool isInvincible = false;
        [SerializeField] public bool isDead;
        [SerializeField] public bool canControll = true;

        [SerializeField] private int shield = 0;
        [SerializeField] private GameObject shield_sprite;

        public KeyCode item1Key = KeyCode.X;
        public KeyCode item2Key = KeyCode.C;


        public UnityEvent<List<BulletClip>> OnBulletClipChanged = new UnityEvent<List<BulletClip>>();

        [SerializeField] private GameObject dash_colider;
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
            shield = 0;
        }

        private void Update()
        {
            CheckImmortal();
            UpdateAnimator();
            if (canControll)
            {
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

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Time.timeScale == 1f)
                {
                    GameManager.Instance.PauseGame();
                }
                else
                {
                    GameManager.Instance.ResumeGame();
                }
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
        public IEnumerator SetInvincible(float time = invincibleCooldown)
        {
            isInvincible = true;
            yield return new WaitForSeconds(time);
            isInvincible = false;
        }

        public void TakeDamage(float damage)
        {
            if (isInvincible) { return; }
            if(shield > 0)
            {
                shield--;
                if(shield == 0)
                {
                    shield_sprite.SetActive(false);
                }
                StartCoroutine(SetInvincible());
                return;
            }

            sizeHandler.Resize(-damage);
            StartCoroutine(SetInvincible());
        }

        [Button]
        public void Die()
        {
            Debug.Log("Die");
            shield_sprite.SetActive(false);
            isDead = true;
            CloseAllFeedbacks();
            deadFeedback.Play(transform, true);
            playerMovement.Freeze();
            sizeHandler.enabled = false;
            canControll = false;
        }

        public void CloseAllFeedbacks()
        {
            allFeedbacks.SetActive(false);
        }

        public void PlayExplosionFeedback()
        {
            explosionFeedback.Play(transform);
        }

        
        public void AddBullet(BulletClip clip)
        {
            playerWeapon.AddBullet(clip);
            OnBulletClipChanged?.Invoke(playerWeapon.bulletClips);
        }

        public bool CanDestroyObstacle()
        {
            return sizeHandler.CanDestroyObstacle();
        }

        public IEnumerator stun(float time)
        {
            _animator.SetTrigger("dizzy");
            canControll = false;
            yield return new WaitForSeconds(time);
            _animator.SetTrigger("Idle");
            canControll |= isDead ? false : true; 
        }

        public bool AddShield()
        {
            if (shield >= 1) return false;
            shield =1;
            shield_sprite.SetActive(true);
            return true;
        }

        public void dash(float speed,float invsibletime)
        {
            SetInvincible(invsibletime);
            playerMovement.dash(speed);
            dash_colider.SetActive(true);
        }
        public void stopdash()
        {
            playerMovement.stopdash();
            dash_colider.SetActive(false);
        }
    }
}