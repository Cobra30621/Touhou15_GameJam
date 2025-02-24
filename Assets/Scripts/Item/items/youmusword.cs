using Player;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class youmusword : BaseItem
{
    [SerializeField] private float existtime = 5f,exInvsibleTime;
    [SerializeField] private float speed = 20f;

    public override bool use()
    {
        if (isusing)
        {
            return false;
        }

        StartCoroutine(dash());
        return true;
    }

    //create iemurator contdown for existtime
    IEnumerator dash()
    {
        print("dash");
        PlayerController.Instance.canControll = false;
        PlayerController.Instance.playerMovement.isdash = true;
        isusing = true;
        PlayerController.Instance.dash(speed,existtime+exInvsibleTime);
        yield return new WaitForSeconds(existtime);
        ItemComplete();
        PlayerController.Instance.stopdash();
        PlayerController.Instance.canControll = true;
        PlayerController.Instance.playerMovement.isdash = false;
    }
}
