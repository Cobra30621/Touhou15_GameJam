﻿using Feedback;
using Player;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace MapObject
{
    public class Obstacle : MonoBehaviour
    {
        
        [SerializeField] private bool customSizeThreshold;
        
        [FormerlySerializedAs("size")]
        [ShowIf("customSizeThreshold")]
        [SerializeField] private float sizeThreshold = 0.5f;

        [Required]
        [SerializeField] private DropBulletObstacle _dropBulletObstacle;

        public bool takeDamage;
        [ShowIf("takeDamage")] 
        public float damage = 0.05f;

        public bool destroyAfterHurtPlayer;

        [SerializeField] private ParticleFeedback playerCollisionFeedback;
        
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var controller = collision.gameObject.GetComponent<PlayerController>();
                OnPlayerEnter(controller);
            }
        }


        private void OnPlayerEnter(PlayerController controller)
        {
            if (PlayerCanDestroyObstacle(controller))
            {
                if(playerCollisionFeedback != null)
                    playerCollisionFeedback.Play(transform);
                
                DestroyByPlayer();
            }
            else
            {
                if (takeDamage)
                {
                    controller.TakeDamage(damage);
                    
                    if (destroyAfterHurtPlayer)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }

        private bool PlayerCanDestroyObstacle(PlayerController controller)
        {
            if (customSizeThreshold)
            {
                return controller.sizeHandler.currentSize >= sizeThreshold;
            }
            else
            {
                return controller.CanDestroyObstacle();
            }
        }
        

        public void DestroyByPlayer()
        {
            _dropBulletObstacle.DestroyByPlayer();
            
            
            
            Destroy(gameObject);
        }
    }
}