using Core;
using Item;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    [SerializeField]
    private GameObject player;

    private PlayerController playerController;

    [SerializeField]
    private ActivateItemIcon activateItemIcon;

    [SerializeField]
    private ItemInfo activatedItem;


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

        GameObject uiObject = GameObject.Find("Canvas");
        if (uiObject != null)
        {
            Transform actTransform = uiObject.transform.Find("ActivateItemIcon");
            if (actTransform != null)
            {
                ActivateItemIcon activateItemIcon = actTransform.GetComponent<ActivateItemIcon>();
            }
        }

        activatedItem = null;
    }

    public void Update()
    {

    }

    public void setActivateItem(ItemInfo item)
    {
        activatedItem = item;
        activateItemIcon.UpdateImage(item);
    }

    public void useActivateItem()
    {
        if (activatedItem == null) return;
        switch (activatedItem.itemType)
        {
            case ItemType.WineGourd:
                playerController.Resize(activatedItem.floatValue);
                break;
            default:
                break;
        }

        setActivateItem(null);
    }
}
