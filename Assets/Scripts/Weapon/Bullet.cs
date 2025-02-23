using Feedback;
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

        [SerializeField] private ParticleFeedback hitFeedback;

        private void Start()
        {
            Destroy(gameObject, lifetime);
        }

        protected virtual void OnHit()
        {
            if (hitFeedback != null)
            {
                hitFeedback.Play(transform);
            }
            
            // 刪除子彈
            Destroy(gameObject);
        }

        public virtual void Initialize(Vector2 dir, Sprite bulletSprite,float speed = 5,bool need_rotate = false)
        {
            _init(dir, speed,need_rotate);
            if (spriteRenderer != null && bulletSprite != null)
            {
                spriteRenderer.sprite = bulletSprite;
            }
        }

        private void _init(Vector2 dir,float speed, bool need_rotate )
        {
            this.speed = speed;
            direction = dir.normalized;
            if(need_rotate) transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        }
        
        public virtual void Initialize(Vector2 dir,float speed = 5, bool need_rotate = false)
        {
            _init(dir, speed, need_rotate);
        }

        protected virtual void Update()
        {
            //Debug.Log($"{(Vector3)(direction)}, {speed} , {Time.deltaTime}");
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
    }

}