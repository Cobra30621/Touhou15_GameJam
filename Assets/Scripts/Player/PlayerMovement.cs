using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]private float[] speed_limit = { 10,1},forcelimit = { 10,1};
        public float moveSpeed = 5f;
        public float jumpForce = 5f;
        private float nowSize;
        private Rigidbody2D rb;
        private bool isGrounded;
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

            // 跳躍控制
            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // 檢查是否接觸地面
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            // 離開地面
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = false;
            }
        }

        private void adjustSpeedForcebySize()
        {
            moveSpeed = (speed_limit[0] - speed_limit[1]) *nowSize +speed_limit[1];
            jumpForce = (forcelimit[0] - forcelimit[1]) * nowSize+forcelimit[1];
        }
    }
}