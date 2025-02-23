using Player;
using Reimu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sakuyaclock : BaseItem
{
    [SerializeField] private float existtime = 5f;
    [SerializeField] private bool isusing = false;
    [SerializeField] private GameObject reimu;
    public override bool use()
    {
        if (isusing)
        {
            return false;
        }
        StartCoroutine( PlayerController.Instance.SetInvincible(existtime));
        if (!reimu.GetComponent<ReimuBattle>().isHit)
        {
            StartCoroutine(inscreentimestop());
        }
        else
        {
            StartCoroutine(outscreentimestop());
        }
        return true;
    }

    IEnumerator outscreentimestop()
    {
        isusing = true;
        reimu.GetComponent<ReimuMovement>().isfreeze = true;
        yield return new WaitForSeconds(existtime);
        reimu.GetComponent<ReimuMovement>().isfreeze = false;
        isusing = false;
    }

    //create iemurator contdown for existtime
    IEnumerator inscreentimestop()
    {
        isusing = true;
        reimu.GetComponent<ReimuBattle>().stopReimuAttack();
        yield return new WaitForSeconds(existtime-2);
        reimu.GetComponent<ReimuBattle>().reimuSprite.SetActive(false);
        yield return new WaitForSeconds(2);
        reimu.GetComponent<ReimuBattle>().startattack();
        isusing = false;
    }
}
