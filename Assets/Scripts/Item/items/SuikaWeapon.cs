using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

public class SuikaWeapon : BaseItem
{
    public GameObject bulletPrefab;

    public override bool use()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, PlayerController.Instance.playerWeapon.shooter.GetFirePoint(), Quaternion.identity);
        var playerPos = PlayerController.Instance.transform.position;

        Bullet bullet = bulletObj.GetComponent<Bullet>();

        var direction = PlayerController.Instance.LeftDirection ? new Vector2(-1, 0) : new Vector2(1, 0);

        Vector2 dir = Quaternion.Euler(0, 0, 0) * direction;

        bullet.Initialize(dir);

        return true;
    }

}
