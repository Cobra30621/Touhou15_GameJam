using System;
using Item;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MapObject
{
    /// <summary>
    /// Item in map that player can gain
    /// </summary>
    public class ItemDrop : MonoBehaviour
    {
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

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            itemInfo = itemData.GetRandomItemWithType(itemType);
            spriteRenderer.sprite = itemInfo.sprite;
        }
        
        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                Debug.Log($"Gain Item {itemInfo}");
                ItemManager.Instance.Effect(itemInfo);
                Destroy(gameObject);
            }
        }
    }
}