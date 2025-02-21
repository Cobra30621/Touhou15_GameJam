using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateItemIcon : MonoBehaviour
{

    [SerializeField] private Image activateItemIcon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateImage(ItemInfo item)
    {
        if (item != null)
        {
            activateItemIcon.sprite = item.sprite;
            activateItemIcon.gameObject.SetActive(true);
        }
        else
        {
            activateItemIcon.gameObject.SetActive(false);
        }
    }
}
