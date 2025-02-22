using Sirenix.OdinInspector;
using UnityEngine;

namespace Weapon
{
    public abstract class Bullet : MonoBehaviour
    {
        public float speed = 5f;
        [Required]
        public SpriteRenderer spriteRenderer;
        public float lifetime = 5f;

        protected Vector2 direction;

        private void Start()
        {
            Destroy(gameObject, lifetime);
        }

        protected virtual void OnHit()
        {
            // TODO 播放特效的代碼
            // ... 播放特效的代碼 ...
            
            // 刪除子彈
            Destroy(gameObject);
        }

        public virtual void Initialize(Vector2 dir, Sprite bulletSprite,float speed = 5)
        {
            this.speed = speed;
            direction = dir.normalized;
            if (spriteRenderer != null && bulletSprite != null)
            {
                spriteRenderer.sprite = bulletSprite;
            }
        }
        
        public virtual void Initialize(Vector2 dir,float speed = 5)
        {
            this.speed = speed;
            direction = dir.normalized;
        }

        protected virtual void Update()
        {
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
    }

}