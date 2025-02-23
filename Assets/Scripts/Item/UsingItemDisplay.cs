using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    public class UsingItemDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject iconPrefab;
        [SerializeField] private Transform container;

        [SerializeField] private ItemData itemData;
        
        
        // Start is called before the first frame update
        void Awake()
        {
            ItemManager.OnUsingItemChanged.AddListener(UpdateUI);
        }

        private void OnDisable()
        {
            ItemManager.OnUsingItemChanged.RemoveListener(UpdateUI);
        }


        private void UpdateUI(Dictionary<ItemType, BaseItem> itemDict)
        {
            foreach (Transform child in container)
            {
                Destroy(child.gameObject);
            }
            
            foreach (var (key, value) in itemDict)
            {
                if (value.isusing)
                {
                    GameObject bulletIcon = Instantiate(iconPrefab, container);
                    var sprite = itemData.GetItemInfo(key).sprite;
                    bulletIcon.GetComponent<Image>().sprite = sprite;
                    
                }
            }
        }
    }
}