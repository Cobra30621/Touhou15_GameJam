using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReimuBoss : MonoBehaviour
{
    public float spellCardTime1;
    public float spellCardTime2;
    public float spellCardTime3;
    public float remainedTime;

    public bool isSpellCard1;

    // Start is called before the first frame update
    void Start()
    {
        StartBossBattle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StartBossBattle()
    {
        // 符卡演出1
        yield return StartCoroutine(StartSpellCardTime1());
        // 符卡演出2
        yield return StartCoroutine(StartSpellCardTime2());
        // 符卡演出3
        yield return StartCoroutine(StartSpellCardTime3());

    }

    public IEnumerator StartSpellCardTime1()
    {
        isSpellCard1 = true;
        yield return new WaitForSeconds(spellCardTime1);
        isSpellCard1 = false;
        
    }

    public IEnumerator StartSpellCardTime2()
    {
        yield return null;
    }

    public IEnumerator StartSpellCardTime3()
    {
        yield return null;
    }
}
