using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

public class spellcard_1_weapon : MonoBehaviour
{
    [SerializeField] private Shooter shooter;

    [SerializeField] private BulletClip bulletClip;

    [SerializeField] private int angle = 0;

    [SerializeField] private float dangle = 5,bdangle = 25;

    [SerializeField] private int Vnumber = 2,Hnumber = 7;

    [SerializeField] private float gapTime = 0.5f,SgapTime= 0.08f;

    [SerializeField] private float speed = 5;
    // Update is called once per frame

    public void OnEnable()
    {
        StartCoroutine(shootAll());
    }

    public void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator shootSingle()
    {
        for (int i = 0; i < Hnumber; i++)
        {
            for (int j = 0; j < Vnumber; j++)
            {
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                shooter.Fire(1,0,direction, speed, true);
            }
            angle += (int)dangle;
            yield return new WaitForSeconds(SgapTime);
        }
    }

    IEnumerator shootAll()
    {
        while (true)
        {
            StartCoroutine(shootSingle());
            yield return new WaitForSeconds(gapTime);
            angle += (int)bdangle;
            angle %= 360;

        }
         
    }
}
