using Core;
using Item;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    [SerializeField]
    private GameObject player;

    private PlayerController playerController;
    
    public int maxItem = 2;
    
    [SerializeField]
    private List<ItemInfo> activatedItems;


    public static UnityEvent<ItemInfo[]> OnItemsChanged = new UnityEvent<ItemInfo[]>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        player = GameObject.Find("Player");

        playerController = player.GetComponent<PlayerController>();

        
       
    }

    private void Start()
    {
        activatedItems = new List<ItemInfo>(maxItem);
        
        for (int i = 0; i < maxItem; i++)
        {
            activatedItems.Add(null);
        }
        
        OnItemsChanged.Invoke(activatedItems.ToArray());
    }


    public void GainItem(ItemInfo item)
    {
        for (int i = 0; i < maxItem; i++)
        {
            if (activatedItems[i] == null)
            {
                activatedItems[i] = item;
                break; // 找到空位後退出循環
            }
        }
        
        OnItemsChanged.Invoke(activatedItems.ToArray());
    }
    
    

    public void useActivateItem(int index)
    {
        if (index < 0 || index >= activatedItems.Count || activatedItems[index] == null) return;

        var item = activatedItems[index];
        
        switch (item.itemType)
        {
            case ItemType.WineGourd:
                PlayerController.Instance.sizeHandler.Resize(item.floatValue);
                break;
            default:
                break;
        }

        activatedItems[index] = null; // 使用道具後設置為 null
        
        OnItemsChanged.Invoke(activatedItems.ToArray());
    }
}
