using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elinPillo : BaseItem
{
    [SerializeField] private float existtime = 5f;
    [SerializeField] private bool isusing = false;
    public override bool use()
    {
        if (isusing)
        {
            return false;
        }

        StartCoroutine(sizefreeze());
        return true;
    }

    //create iemurator contdown for existtime
    IEnumerator sizefreeze()
    {
        print("elin");
        PlayerController.Instance.sizeHandler.sizefreeze = true;
        isusing = true;
        yield return new WaitForSeconds(existtime);
        PlayerController.Instance.sizeHandler.sizefreeze = false;
        isusing = false;
    }
}
