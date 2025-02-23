using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Item
{
    /// <summary>
    /// Data associated with the item.
    /// </summary>
    [CreateAssetMenu(fileName = "Item Data", menuName = "Item Data")]
    public class ItemData : ScriptableObject
    {
        /// <summary>
        /// List of all items
        /// </summary>
        public List<ItemInfo> ItemInfos;

        /// <summary>
        /// Gets a random item of the specified type.
        /// </summary>
        /// <param name="itemType">The type of item to retrieve.</param>
        /// <returns>A random item of the specified type.</returns>
        public ItemInfo GetRandomItemWithType(ItemType itemType)
        {
            var items = ItemInfos.Where(info => info.itemType == itemType).ToList();

            if (items.Count == 0)
            {
                Debug.LogError($"ItemData does not contain item with {itemType} ItemType");
                return new ItemInfo();
            }
            
            return items[UnityEngine.Random.Range(0, items.Count)];
            
        }

        public ItemInfo GetRandomItemWithoutWineGourd()
        {
            var items = ItemInfos.Where(info => 
                info.itemType != ItemType.WineGourd
                ).ToList();

            if (items.Count == 0)
            {
                Debug.LogError($"ItemData does not contain enough item");
                return new ItemInfo();
            }
            
            return items[UnityEngine.Random.Range(0, items.Count)];
        }

        public ItemInfo GetItemWithID(int id)
        {
            return ItemInfos[id];

        }

        public ItemInfo GetItemInfo(ItemType itemType)
        {
            var items = ItemInfos.Where(info => info.itemType == itemType).ToList();

            if (items.Count == 0)
            {
                Debug.LogError($"ItemData does not contain item with {itemType} ItemType");
                return new ItemInfo();
            }
            
            return items[0];
        }


        /// <summary>
        /// Gets a random item from the list of items.
        /// </summary>
        /// <returns>A random item.</returns>
        public ItemInfo GetRandomItem()
        {
            return ItemInfos[UnityEngine.Random.Range(0, ItemInfos.Count)];
        }
    }

    [Serializable]
    public class ItemInfo
    {
        /// <summary>
        /// The type of the item.
        /// </summary>
        public ItemType itemType;

        /// <summary>
        /// The title of the item.
        /// </summary>
        public string title;

        /// <summary>
        /// A description of the item.
        /// </summary>
        public string description;

        public int intValue;

        public float floatValue;


        /// <summary>
        /// The sprite representing the item.
        /// </summary>
        public Sprite sprite;

        public override string ToString()
        {
            return
                $"{nameof(itemType)}: {itemType}, {nameof(title)}: {title}";
        }
    }
}