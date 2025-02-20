using Core;
using Item;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
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
    private Image ActivateItemIcon;

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
                Transform imageTransform = actTransform.Find("Image");
                if (imageTransform != null)
                {
                    GameObject imageObject = imageTransform.gameObject;
                }
            }
        }

        activatedItem = null;
    }

    public void Update()
    { 
    
    }

    public void setActivateItem(ItemInfo item) {
        activatedItem = item;
        if (activatedItem != null)
        {
            ActivateItemIcon.sprite = item.sprite;
            ActivateItemIcon.gameObject.SetActive(true);
        } else
        {
            ActivateItemIcon.gameObject.SetActive(false);
        }
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
