using Core;
using Item;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
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

    private List<ItemInfo> activatedItems;

    public GameObject itemlist;

    public Dictionary<ItemType, BaseItem> itemeffect;


    public static UnityEvent<ItemInfo[]> OnItemsChanged = new UnityEvent<ItemInfo[]>();

    
    public static UnityEvent<Dictionary<ItemType, BaseItem>> OnUsingItemChanged = 
        new UnityEvent<Dictionary<ItemType, BaseItem>>();
    
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

        itemeffect = new Dictionary<ItemType, BaseItem>()
        {
            {ItemType.WineGourd, itemlist.GetComponent<WineGourd>()},
            {ItemType.MarisaFly, itemlist.GetComponent<MarisaFly>()},
            {ItemType.SakuyaClock, itemlist.GetComponent<sakuyaclock>()},
            {ItemType.elinPillo, itemlist.GetComponent<elinPillo>()},
            {ItemType.ayaSpeedUp, itemlist.GetComponent<ayaspeedup>()},
            {ItemType.suikaWeapon, itemlist.GetComponent<SuikaWeapon>()},
            {ItemType.reimupenny, itemlist.GetComponent<reimupenny>()  },
            {ItemType.shield, itemlist.GetComponent<shield>()}
        };

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


    public bool GainItem(ItemInfo item)
    {
        for (int i = 0; i < maxItem; i++)
        {
            if (activatedItems[i] == null)
            {
                print(item.itemType + "gain");
                activatedItems[i] = item;
                OnItemsChanged.Invoke(activatedItems.ToArray());
                return true; // 找到空位後退出循環
            }
        }

        OnItemsChanged.Invoke(activatedItems.ToArray());
        return false;
    }


    
    public void useActivateItem(int index)
    {
        if (index < 0 || index >= activatedItems.Count || activatedItems[index] == null) return;

        var item = activatedItems[index];

        if (itemeffect.ContainsKey(item.itemType) && itemeffect[item.itemType].use())
        {
            activatedItems[index] = null; // 使用道具後設置為 null
        }
        OnItemsChanged.Invoke(activatedItems.ToArray());
        
        InvokeActiveItemChanged();
    }

    [Button]
    public void Test_UseItem(ItemType itemType)
    {
        itemeffect[itemType].use();
        
        InvokeActiveItemChanged();
    }
    
    public void InvokeActiveItemChanged()
    {
        OnUsingItemChanged.Invoke(itemeffect);
    }
}
