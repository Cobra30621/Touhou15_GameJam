using Core;
using Item;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    [SerializeField] private GameObject player;
    private PlayerController playerController;
    private Dictionary<ItemType, Action<PlayerController>> itemEffects;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // ½T«O Singleton
        }

        playerController = player.GetComponent<PlayerController>();

        itemEffects = new Dictionary<ItemType, Action<PlayerController>>
        {
            { ItemType.Bigger, playerController => playerController.Grow(0.1f) },
        };

    }

    public void Update()
    { 
    
    }

    public void Effect(ItemInfo item) {
        if (itemEffects.TryGetValue(item.itemType, out Action<PlayerController> effect))
        {
            effect(playerController);
            Debug.Log($"¾A¥Î {item.itemType} effect");
        }
        else
        {
            Debug.LogWarning($"No effect defined for {item.itemType}.");
        }
    }
}
