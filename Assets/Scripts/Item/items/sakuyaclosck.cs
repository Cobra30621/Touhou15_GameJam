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
        if(reimu.GetComponent<ReimuBattle>().IsRunning)StartCoroutine(outscreentimestop());
        
        return true;
    }

    IEnumerator outscreentimestop()
    {
        isusing = true;
        reimu.GetComponent<ReimuMovement>().isfreeze = true;
        yield return new WaitForSeconds(existtime);
        reimu.GetComponent<ReimuMovement>().isfreeze = false;
        ItemComplete();
    }

}
