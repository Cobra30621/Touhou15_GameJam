using Player;
using UnityEngine;
using Weapon;

namespace MapObject
{
    public class DropBulletObstacle : MonoBehaviour
    {
        public float dropBulletRate = 0.3f;
        [SerializeField] private SpriteRenderer _renderer;
        
        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        
        public void DestroyByPlayer()
        {
            // random
            float random = Random.Range(0f, 1f);
            if (random <= dropBulletRate)
            {
                DropBullet();
            }
        }

        
        private void DropBullet()
        {
            var sprite = _renderer.sprite;
            
            // play feedback
            
            
            BulletClip clip = new BulletClip()
            {
                Sprite = sprite,
            };
            PlayerController.Instance.AddBullet(clip);
        }
        
    }
}