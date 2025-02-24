using Reimu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dash_colider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("collision");
        if (collision.gameObject.CompareTag("Reimu"))
        {
            print("Reimu hit");
            collision.gameObject.GetComponent<ReimuHitTrigger>().OnHit(2);
        }
    }

}
