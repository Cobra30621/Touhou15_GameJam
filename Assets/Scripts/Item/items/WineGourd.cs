using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineGourd : BaseItem
{
    [SerializeField]private float size = 0.1f;
    public override bool use()
    {
        PlayerController.Instance.sizeHandler.Resize(size);
        return true;
    }
}
