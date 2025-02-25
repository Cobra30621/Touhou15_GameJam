using Core;
using Fungus;
using Player;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class ReimuBoss : MonoBehaviour
{
    public float spellCardTime1;
    public float spellCardTime2;
    public float spellCardTime3;
    public float remainedTime;
    public GameObject reimuSprite;

    public GameObject health;

    public bool isSpellCard1;

    public Coroutine currentRoutine;
    public int spellCardID;

    public bool isHit;
    public int HP;

    public AudioSource audioSource; // 在 Inspector 內指定
    public AudioClip newClip;       // 新的音樂片段

    void ChangeMusic()
    {
        audioSource.clip = newClip; // 替換音樂
        audioSource.Play();         // 播放新的音樂
    }

    public void GoBack() {
        GameObject.Find("bossRoom").GetComponent<bossRoomController>().vcam.m_Lens.OrthographicSize = 7;
        GameObject.Find("bossRoom").GetComponent<bossRoomController>().vcam.m_Lens.LensShift = new Vector3(0, 3, 0);
        PlayerController.Instance.transform.position = GameObject.Find("bossRoom").GetComponent<bossRoomController>().positionBeforeBoss;
        StopAllCoroutines();
        gameObject.SetActive(false);
        ChangeMusic();
        enabled = false;
    }

    public IEnumerator OnHitCoroutine()
    {
        isHit = true;
        HP -= 1;
        health.GetComponent<ShowHealth>().UpdateHealth(HP);
        if (HP == 0)
        {
            MainCanvas.Instance.DisplayClock(false);
            PlayerController.Instance.sizeHandler.Resize(0.3f);
            StopAllCoroutines();
            Cleaner();
            if (spellCardID == 1)
            {
                spellCardID = 2;
                yield return new WaitForSeconds(3f);
                currentRoutine = StartCoroutine(StartSpellCardTime2());
            }
            else if (spellCardID == 2)
            {
                
                spellCardID = 3;
                yield return new WaitForSeconds(3f);
                currentRoutine = StartCoroutine(StartSpellCardTime3());
            }
            else if (spellCardID == 3)
            {
                spellCardID = 0;
                GoBack();
            }
        }
        yield return new WaitForSeconds(2f);
        isHit = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        health.SetActive(true);
        spellCardID = 1;
        currentRoutine = StartCoroutine(StartSpellCardTime1());
    }

    // Update is called once per frame
    void Update()
    {
        remainedTime -= Time.deltaTime;
        MainCanvas.Instance.clock.text = remainedTime.ToString("F1");
        if (isHit)
        {
        reimuSprite.GetComponent<SpriteRenderer>().enabled = !reimuSprite.GetComponent<SpriteRenderer>().enabled;
        }
        else if (!reimuSprite.GetComponent<SpriteRenderer>().enabled)
        {
            reimuSprite.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void Cleaner()
    {
        GameObject.Find("bossRoom").GetComponent<bossRoomController>().spell1.Disable();
        GameObject.Find("bossRoom").GetComponent<bossRoomController>().spell2.Disable();
        GameObject.Find("bossRoom").GetComponent<bossRoomController>().spell3.Disable();

    }

    public IEnumerator StartSpellCardTime1()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(MainCanvas.Instance.ShowSpellCard("Spirit Sign \"Dream Seal -Spread-\""));
        HP = 3;
        health.GetComponent<ShowHealth>().UpdateHealth(HP);
        GameObject.Find("bossRoom").GetComponent<bossRoomController>().spell1.enabled = true;
        StartCoroutine(GameObject.Find("bossRoom").GetComponent<bossRoomController>().spell1.SpellStart());
        remainedTime = spellCardTime1;
        MainCanvas.Instance.DisplayClock(true);
        yield return new WaitForSeconds(spellCardTime1);
        MainCanvas.Instance.DisplayClock(false);
        GameManager.Instance.EnterBadEnd();

    }



    public IEnumerator StartSpellCardTime2()
    {
        StartCoroutine(MainCanvas.Instance.ShowSpellCard("Innate Dream"));
        HP = 3;
        health.GetComponent<ShowHealth>().UpdateHealth(HP);
        GameObject.Find("bossRoom").GetComponent<bossRoomController>().spell2.enabled = true;
        StartCoroutine(GameObject.Find("bossRoom").GetComponent<bossRoomController>().spell2.SpellStart());
        remainedTime = spellCardTime2;
        MainCanvas.Instance.DisplayClock(true);
        yield return new WaitForSeconds(spellCardTime2);
        MainCanvas.Instance.DisplayClock(false);
        GameManager.Instance.EnterBadEnd();
    }

    public IEnumerator StartSpellCardTime3()
    {
        StartCoroutine(MainCanvas.Instance.ShowSpellCard("Boundary \"Spiral Danmaku Barrier\""));
        HP = 3;
        health.GetComponent<ShowHealth>().UpdateHealth(HP);
        GameObject.Find("bossRoom").GetComponent<bossRoomController>().spell3.enabled = true;
        StartCoroutine(GameObject.Find("bossRoom").GetComponent<bossRoomController>().spell3.SpellStart());
        remainedTime = spellCardTime3;
        MainCanvas.Instance.DisplayClock(true);
        yield return new WaitForSeconds(spellCardTime3);
        MainCanvas.Instance.DisplayClock(false);
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
