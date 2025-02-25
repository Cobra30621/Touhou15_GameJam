using Player;
using Reimu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sakuyaclock : BaseItem
{
    [SerializeField] private float existtime = 5f;
    [SerializeField] private GameObject reimu;
    public override bool use()
    {

        if (isusing)
        {
            return false;
        }
        if (reimu.GetComponent<ReimuBattle>().enabled)
        {
            if(!reimu.GetComponent<ReimuBattle>().IsRunning)StartCoroutine(outscreentimestop());
        }
        else
        {
            if(!reimu.GetComponent<ReimuBoss>().isHit)StartCoroutine(stopspell());
        }


            return true;
    }

    IEnumerator stopspell()
    {
        reimu.GetComponent<ReimuBoss>().timefreeze = true;
        yield return new WaitForSeconds(existtime);
        reimu.GetComponent<ReimuBoss>().timefreeze = false;
        ItemComplete();
    }

    IEnumerator outscreentimestop()
    {
        isusing = true;
        Debug.Log("Clock Test");
        Debug.Log(reimu.GetComponent<ReimuMovement>().isfreeze);
        reimu.GetComponent<ReimuMovement>().isfreeze = true;
        Debug.Log(reimu.GetComponent<ReimuMovement>().isfreeze);
        yield return new WaitForSeconds(existtime);
        reimu.GetComponent<ReimuMovement>().isfreeze = false;
        ItemComplete();
    }

}
