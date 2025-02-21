using Sirenix.OdinInspector;
using UnityEngine;

namespace Weapon
{
    /// <summary>
    /// 抽象子彈類別，負責子彈的基本行為和屬性。
    /// </summary>
    public abstract class Bullet : MonoBehaviour
    {
        /// <summary>
        /// 子彈的速度。
        /// </summary>
        public float speed = 5f;

        /// <summary>
        /// 用於顯示子彈的精靈渲染器。
        /// </summary>
        [Required]
        public SpriteRenderer spriteRenderer;

        /// <summary>
        /// 子彈的生命週期，秒數。
        /// </summary>
        public float lifetime = 5f;

        protected Vector2 direction;

        private void Start()
        {
            // 在生命週期結束時銷毀子彈
            Destroy(gameObject, lifetime);
        }

        /// <summary>
        /// 當子彈命中目標時調用的虛擬方法。
        /// </summary>
        protected virtual void OnHit()
        {
            // TODO 播放特效的代碼
            // ... 播放特效的代碼 ...
            
            // 刪除子彈
            Destroy(gameObject);
        }

        /// <summary>
        /// 初始化子彈的方向和精靈。
        /// </summary>
        /// <param name="dir">子彈的方向。</param>
        /// <param name="bulletSprite">子彈的精靈。</param>
        public virtual void Initialize(Vector2 dir, Sprite bulletSprite)
        {
            direction = dir.normalized;
            if (spriteRenderer != null && bulletSprite != null)
            {
                spriteRenderer.sprite = bulletSprite;
            }
        }
        
        /// <summary>
        /// 初始化子彈的方向。
        /// </summary>
        /// <param name="dir">子彈的方向。</param>
        public virtual void Initialize(Vector2 dir)
        {
            direction = dir.normalized;
        }

        /// <summary>
        /// 更新子彈的位置。
        /// </summary>
        protected virtual void Update()
        {
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
    }

}