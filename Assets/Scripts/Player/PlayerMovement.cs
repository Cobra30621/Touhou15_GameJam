using Sirenix.OdinInspector.Editor.Validation;
using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]private float[] speed_limit = {20,5},forcelimit = {20,5};
        public float moveSpeed = 10f;
        public float jumpForce = 10f;
        private float nowSize;
        private Rigidbody2D rb;
        [SerializeField] private bool isGrounded;
        [SerializeField] private bool enableJump;
        [SerializeField] private bool isJumping;
        public PlayerSizeHandler playersize;



        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            nowSize = playersize.currentSize;
        }

        void Update()
        {
            nowSize = playersize.currentSize;
            adjustSpeedForcebySize();

            // 移動控制
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            CheckGrounded();

            // 跳躍控制
            if (enableJump && isGrounded && (Input.GetButton("Jump") || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
            {
                enableJump = false;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }

            // 離開地面，
            if (!isGrounded && !enableJump)
            {
                isJumping = true;
            }

            if (isGrounded && isJumping)
            {
                isJumping = false;
                enableJump = true;
            }

            //使用主動道具
            if (Input.GetKeyDown(KeyCode.E))
            {
                UseActivateItem();
            }

        }

        public void Freeze()
        {
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0;
        }

        void CheckGrounded()
        {
            Bounds bounds = GetComponent<Collider2D>().bounds;
            Vector2 boxSize = new Vector2(bounds.size.x * 0.9f, 0.1f); // 設定較寬的區域
            Vector2 checkPosition = (Vector2)transform.position + Vector2.down * (bounds.extents.y + 0.05f);

            isGrounded = Physics2D.OverlapBox(checkPosition, boxSize, 0, LayerMask.GetMask("Obstacle"));

            // Debug 用
            Debug.DrawRay(checkPosition, Vector2.down * 0.1f, Color.green);

        }

        private void UseActivateItem() {
            ItemManager.Instance.useActivateItem();
        }
        
        private void adjustSpeedForcebySize()
        {
            moveSpeed = (speed_limit[0] - speed_limit[1]) * nowSize + speed_limit[1];
            jumpForce = (forcelimit[0] - forcelimit[1]) * nowSize + forcelimit[1];

        }

        public float GetVelocityX()
        {
            return rb.velocity.x;
        }
    }
}