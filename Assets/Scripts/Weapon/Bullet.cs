using UnityEngine;

namespace Weapon
{
    public abstract class Bullet : MonoBehaviour
    {
        public float speed = 5f; 
        public SpriteRenderer spriteRenderer;

        protected Vector2 direction;

        
        public virtual void Initialize(Vector2 dir, Sprite bulletSprite)
        {
            direction = dir.normalized;
            if (spriteRenderer != null && bulletSprite != null)
            {
                spriteRenderer.sprite = bulletSprite;
            }
        }
        
        public virtual void Initialize(Vector2 dir)
        {
            direction = dir.normalized;
        }

        protected virtual void Update()
        {
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
    }

}