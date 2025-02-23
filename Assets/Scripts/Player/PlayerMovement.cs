using Sirenix.OdinInspector.Editor.Validation;
using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Force")]
        [SerializeField] private float[] speed_limit = {20,5},forcelimit = {20,5};
        [SerializeField] public float moveSpeed = 10f;
        [SerializeField] public float jumpForce = 10f;

        private float nowSize;

        
        private Rigidbody2D rb;
        [SerializeField] private bool isGrounded;
        [SerializeField] private bool enableJump;
        public PlayerSizeHandler playersize;

        
        public bool leftDirection;
        
        public float idleTimer = 0f; // 新增計時器

        public float moveInput;
        
        public bool infjump = false;
        
        

        public float speed_adjust = 0f, force_adjust = 0f;
        
        [Required]
        public ParticleSystem infJumpEffect;
        [Required]
        public ParticleSystem speedUpEffect;
        
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            nowSize = playersize.currentSize;
        }


        

        void Update()
        {
            if (PlayerController.Instance.isDead) return;
            nowSize = playersize.currentSize;
            adjustSpeedForce();

            // 移動控制
            if (PlayerController.Instance.canControll) moveInput = Input.GetAxis("Horizontal");
            else moveInput = 0;
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            CheckGrounded();

            // 跳躍控制
            if (PlayerController.Instance.canControll&&enableJump &&( isGrounded||infjump) && (Input.GetButton("Jump") || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
            {
                enableJump = false;
                StartCoroutine(JumpCooldown());
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }

            // 玩家水平翻轉
            if (moveInput < 0) // 當玩家向左移動
            {
                leftDirection = true; // 水平翻轉
            }
            else if (moveInput > 0) // 當玩家向右移動
            {
                leftDirection = false; // 恢復正常
            }

            // 更新動畫狀態
            if (moveInput == 0 && PlayerController.Instance.canControll)
            {
                idleTimer += Time.deltaTime; // 增加計時器
            }
            else
            {
                idleTimer = 0f; // 重置計時器
            }
        }

        public IEnumerator JumpCooldown() {
            yield return new WaitForSeconds(0.1f);
            enableJump = true;
        }
        public void Freeze()
        {
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0;
        }

        void CheckGrounded()
        {
            Bounds bounds = GetComponent<Collider2D>().bounds;
            Vector2 boxSize = new Vector2(bounds.size.x * 0.8f, 0.1f); // 設定較寬的區域
            Vector2 checkPosition = (Vector2)bounds.center + Vector2.down * (bounds.extents.y + 0.05f);

            isGrounded = Physics2D.OverlapBox(checkPosition, boxSize, 0, LayerMask.GetMask("Obstacle"));

            // Debug 用
            Debug.DrawRay(checkPosition, Vector2.down * 0.1f, Color.green);

        }
        
        public void SetInfJump(bool enable)
        {
            infjump = enable;
            infJumpEffect.gameObject.SetActive(enable);
            
            infJumpEffect.Play();
        }
        
        public void SetSpeedUp(float speed, float force)
        {
            speed_adjust = speed;
            force_adjust = force;
            
            speedUpEffect.gameObject.SetActive(true);
            speedUpEffect.Play();
        }

        public void ResetSpeedUp()
        {
            speed_adjust = 0;
            force_adjust = 0;
            
            speedUpEffect.gameObject.SetActive(false);
        }
        
        
        private void adjustSpeedForce()
        {
            moveSpeed = (speed_limit[0] - speed_limit[1]) * nowSize + speed_limit[1]+speed_adjust;
            jumpForce = (forcelimit[0] - forcelimit[1]) * nowSize + forcelimit[1]+force_adjust;

        }

        public float GetVelocityX()
        {
            return rb.velocity.x;
        }
    }
}