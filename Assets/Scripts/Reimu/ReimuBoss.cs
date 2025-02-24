using Core;
using Fungus;
using Player;
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

    public Coroutine currentRoutine;
    public int spellCardID;

    public bool isHit;
    public int HP;

    public void GoBack() {
        PlayerController.Instance.transform.position = GameObject.Find("bossRoom").GetComponent<bossRoomController>().positionBeforeBoss;
        gameObject.SetActive(false);
    }

    public IEnumerator OnHitCoroutine()
    {
        isHit = true;
        HP -= 1;
        if (HP == 0)
        {
            StopCoroutine(currentRoutine);
            yield return StartCoroutine(Cleaner());
            if (spellCardID == 1)
            {
                GoBack();
                Debug.Log("COOL");
                spellCardID = 2;
                currentRoutine = StartCoroutine(StartSpellCardTime2());
            }
            else if (spellCardID == 2)
            {
                spellCardID = 3;
                currentRoutine = StartCoroutine(StartSpellCardTime3());
            }
            else if (spellCardID == 3)
            {
                spellCardID = 0;
                // currentRoutine = StartCoroutine(EndSpellCard());
            }
        }
        yield return new WaitForSeconds(3f);
        isHit = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        spellCardID = 1;
        currentRoutine = StartCoroutine(StartSpellCardTime1());
    }

    // Update is called once per frame
    void Update()
    {
        if (isHit)
        {
        reimuSprite.GetComponent<SpriteRenderer>().enabled = !reimuSprite.GetComponent<SpriteRenderer>().enabled;
        }
        else if (!reimuSprite.GetComponent<SpriteRenderer>().enabled)
        {
            reimuSprite.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public IEnumerator Cleaner()
    {
        // clean code
        yield return null;
    }

    public IEnumerator StartSpellCardTime3()
    {
        HP = 3;
        isSpellCard1 = true;
        yield return new WaitForSeconds(spellCardTime1);
        isSpellCard1 = false;
        
    }



    public IEnumerator StartSpellCardTime2()
    {
        HP = 3;
        yield return null;
    }

    public IEnumerator StartSpellCardTime1()
    {
        HP = 3;
        GameObject.Find("bossRoom").GetComponent<bossRoomController>().spell3.enabled = true;
        StartCoroutine(GameObject.Find("bossRoom").GetComponent<bossRoomController>().spell3.SpellStart());
        yield return new WaitForSeconds(spellCardTime3);
        GameManager.Instance.EnterBadEnd();
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
