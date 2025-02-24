using Player;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class shield : BaseItem
{
    public override bool use()
    {
        return PlayerController.Instance.AddShield();
    }

}
