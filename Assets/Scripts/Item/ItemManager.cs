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

    private List<ItemInfo> activatedItems;

    public GameObject itemlist;

    public Dictionary<ItemType, BaseItem> itemeffect;


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

        itemeffect = new Dictionary<ItemType, BaseItem>()
        {
        {ItemType.WineGourd, itemlist.GetComponent<WineGourd>()},
        {ItemType.MarisaFly, itemlist.GetComponent<MarisaFly>()},
            {ItemType.SakuyaClock, itemlist.GetComponent<sakuyaclock>()},
            {ItemType.elinPillo, itemlist.GetComponent<elinPillo>()},
            {ItemType.ayaSpeedUp, itemlist.GetComponent<ayaspeedup>()},
            {ItemType.reimupenny, itemlist.GetComponent<reimupenny>()  }
        };

    }

    private void Start()
    {
        activatedItems = new List<ItemInfo>(maxItem);

        for (int i = 0; i < maxItem; i++)
        {
            activatedItems.Add(null);
        }
        foreach (var item in activatedItems)
        {
            print(item);
        }

        OnItemsChanged.Invoke(activatedItems.ToArray());
    }


    public void GainItem(ItemInfo item)
    {
        for (int i = 0; i < maxItem; i++)
        {
            if (activatedItems[i] == null)
            {
                print(item.itemType + "gain");
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

        if (itemeffect.ContainsKey(item.itemType) && itemeffect[item.itemType].use())
        {
            activatedItems[index] = null; // 使用道具後設置為 null
        }
        OnItemsChanged.Invoke(activatedItems.ToArray());
    }
}
