using System;
using Feedback;
using Item;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;

namespace MapObject
{
    /// <summary>
    /// Item in map that player can gain
    /// </summary>
    public class ItemDrop : MonoBehaviour
    {
        public bool random;
        
        /// <summary>
        /// The type of item that can be dropped.
        /// </summary>
        public ItemType itemType;

        /// <summary>
        /// Data associated with the item.
        /// </summary>
        [Required]
        [SerializeField] private ItemData itemData;

        /// <summary>
        /// The sprite renderer for displaying the item.
        /// </summary>
        [SerializeField] private SpriteRenderer spriteRenderer;

        /// <summary>
        /// Information about the item being dropped.
        /// </summary>
        private ItemInfo itemInfo;

        [SerializeField]
        private Feedback.ParticleFeedback particleFeedback;

        public bool respawn;
        
        [ShowIf("respawn")]
        public float respawnTime = 5f;
        
        private bool isRespawning;
        private Collider2D itemCollider;

        private void Awake()
        {
            itemCollider = GetComponent<Collider2D>();
            Init();
        }

        private void Init()
        {
            if (random)
            {
                itemInfo = itemData.GetRandomItemWithoutWineGourd();
            }
            else
            {
                itemInfo = itemData.GetRandomItemWithType(itemType);
            }
            spriteRenderer.sprite = itemInfo.sprite;
        }


        private void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                GainItem();
            }
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                GainItem();
            }
        }

        [Button]
        private void GainItem()
        {
            if (ItemManager.Instance.GainItem(itemInfo))
            {
                Debug.Log($"Gain Item {itemInfo}");
                particleFeedback.Play(transform);
                
                if (respawn && !isRespawning)
                {
                    StartCoroutine(RespawnRoutine());
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        
        private IEnumerator RespawnRoutine()
        {
            isRespawning = true;
            spriteRenderer.enabled = false;
            itemCollider.enabled = false;
            
            yield return new WaitForSeconds(respawnTime);
            
            spriteRenderer.enabled = true;
            itemCollider.enabled = true;
            isRespawning = false;
        }
    }
}