using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    public virtual bool use() { return true; }

    public bool isusing = false;

    public void ItemComplete()
    {
        isusing = false;
        ItemManager.Instance.InvokeActiveItemChanged();
    }
}
