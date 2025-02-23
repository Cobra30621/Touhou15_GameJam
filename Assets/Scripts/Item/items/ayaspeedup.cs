using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ayaspeedup : BaseItem
{
    [SerializeField] private float existtime = 5f;
    [SerializeField] private Vector2 force = new Vector2(0, 0);
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
        PlayerController.Instance.playerMovement.SetSpeedUp(force.y, force.x);
        
        isusing = true;
        yield return new WaitForSeconds(existtime);
        PlayerController.Instance.playerMovement.ResetSpeedUp();

        ItemComplete();
    }
}
