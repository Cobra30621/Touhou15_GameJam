using Player;
using Reimu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reimupenny : BaseItem
{
    [SerializeField] private float existtime = 5f;
    [SerializeField] private float speedFactor = 0.4f;
    [SerializeField] private GameObject reimu;
    [SerializeField] private GameObject reimusprite;

    public override bool use()
    {
        if (isusing)
        {
            return false;
        }
        if (reimu.GetComponent<ReimuBattle>().IsRunning)
        {
            reimusprite.GetComponent<ReimuHitTrigger>().OnHit();
        }
        StartCoroutine(Reimuslow());

        return true;
    }

    //create iemurator contdown for existtime
    IEnumerator Reimuslow()
    {
        isusing = true;
        reimu.GetComponent<ReimuMovement>().speedFactor = speedFactor;
        yield return new WaitForSeconds(existtime);
        reimu.GetComponent<ReimuMovement>().speedFactor = 1f;
        ItemComplete();
    }
}
