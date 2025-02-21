using System;
using Item;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] private Image [] itemIcons;
        // Start is called before the first frame update
        void Awake()
        {
            ItemManager.OnItemsChanged.AddListener(UpdateUI);
        }


        private void OnDisable()
        {
            ItemManager.OnItemsChanged.RemoveListener(UpdateUI);
        }


        private void UpdateUI(ItemInfo [] items)
        {
            for (int i = 0; i < itemIcons.Length; i++)
            {
                var itemIcon = itemIcons[i];
                var item = items[i];

                if (item != null)
                {
                    itemIcon.sprite = item.sprite;
                    itemIcon.gameObject.SetActive(true);
                }
                else
                {
                    itemIcon.gameObject.SetActive(false);
                }
            }
        }
    }
}