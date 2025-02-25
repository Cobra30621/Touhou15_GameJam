using Player;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class youmusword : BaseItem
{
    [SerializeField] private float existtime = 5f,existtime2 = 2f,exInvsibleTime;
    [SerializeField] private float speed = 20f,speed2 = 9f;
    [SerializeField] private GameObject reimu;

    public override bool use()
    {
        if (isusing)
        {
            return false;
        }
        if(reimu.GetComponent<ReimuBattle>().enabled)StartCoroutine(dash(speed));
        else StartCoroutine(dash(speed2));
        return true;
    }

    //create iemurator contdown for existtime
    IEnumerator dash(float speed)
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
