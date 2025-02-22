using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootObstacleActive : MonoBehaviour
{

     [SerializeField]private bool isShooting = false;
     [SerializeField]GameObject weapon;
    [SerializeField] float distance = 10f;
    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance( PlayerController.Instance.transform.position,transform.position)<distance)
        {
            if (!isShooting)
            {
                isShooting = true;
                weapon.SetActive(true);
            }
        }
        else
        {
            if (isShooting)
            {
                isShooting = false;
                weapon.SetActive(false);
            }
        }
    }
}
