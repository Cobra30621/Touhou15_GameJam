using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaFly : BaseItem
{
    [SerializeField] private float existtime = 5f;
    public override bool use()
    {
        if (isusing)
        {
            return false;
        }

        StartCoroutine(fly());
        return true;
    }

    //create iemurator contdown for existtime
    IEnumerator fly()
    {
        print("MarisaFly");
        PlayerController.Instance.playerMovement.infjump = true;
        isusing = true;
        yield return new WaitForSeconds(existtime);
        PlayerController.Instance.playerMovement.infjump = false;
        ItemComplete();
    }
}
