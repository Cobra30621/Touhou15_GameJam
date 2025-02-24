using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReimuBoss : MonoBehaviour
{
    public float spellCardTime1;
    public float spellCardTime2;
    public float spellCardTime3;
    public float remainedTime;
    public GameObject reimuSprite;

    public bool isSpellCard1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartBossBattle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StartBossBattle()
    {
        // 符卡演出1
        Debug.Log("A");
        yield return StartCoroutine(StartSpellCardTime1());
        Debug.Log("B");
        // 符卡演出2
        yield return StartCoroutine(StartSpellCardTime2());
        Debug.Log("C");
        // 符卡演出3
        yield return StartCoroutine(StartSpellCardTime3());
        Debug.Log("D");

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
        GameObject.Find("bossRoom").GetComponent<bossRoomController>().spell3.enabled = true;
        yield return StartCoroutine(GameObject.Find("bossRoom").GetComponent<bossRoomController>().spell3.SpellStart());
        yield return new WaitForSeconds(spellCardTime1);
        GameObject.Find("bossRoom").GetComponent<bossRoomController>().spell3.enabled = false;
    }

    public IEnumerator SmoothMoveCoroutine(Vector3 startPosition, Vector3 endPosition, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            reimuSprite.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
