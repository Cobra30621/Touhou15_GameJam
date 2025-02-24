using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

public class SuikaWeapon : BaseItem
{
    [SerializeField] int active_num = 0;
    [SerializeField] private float existtime = 5f;
    [SerializeField] private BulletClip bullet;
    public override bool use()
    {
        PlayerController.Instance.AddBullet(bullet);
        StartCoroutine(adddamage());
        return true;
    }
    IEnumerator adddamage()
    {
        active_num++;
        PlayerController.Instance.playerWeapon.damage = 2;
        isusing = true;
        yield return new WaitForSeconds(existtime);
        active_num--;
        
        if (active_num == 0)
        {
            PlayerController.Instance.playerWeapon.damage = 1;
            ItemComplete();
            isusing = false;
        }
    }

}
