using Player;
using System.Collections;
using System.Collections.Generic;
using Dialog;
using TMPro;
using UI;
using UnityEngine;

public class BeforeBoss : MonoBehaviour
{
    public GameObject reimu;
    public GameObject reimuOutShooter;
    public GameObject reimuSprite;
    public Vector3 startPosition;
    public Vector3 endPosition;
    public Vector3 bossRoomLocation;
    public AudioSource audioSource; // 在 Inspector 內指定
    public AudioClip newClip;       // 新的音樂片段
    public Sprite ReimuHandsup;

    void ChangeMusic()
    {
        audioSource.clip = newClip; // 替換音樂
        audioSource.Play();         // 播放新的音樂
    }

    // Start is called before the first frame update
    void Start()
    {
        reimu = GameObject.Find("Reimu");
        reimuOutShooter = reimu.transform.Find("OutShooter").gameObject;
        reimuSprite = reimu.transform.Find("ReimuSprite").gameObject;
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            StartCoroutine(BeforeBossShow());
        }
    }

    IEnumerator BeforeBossShow()
    {
        GameObject.Find("bossRoom").GetComponent<bossRoomController>().vcam.m_Lens.OrthographicSize = 9;
        GameObject.Find("bossRoom").GetComponent<bossRoomController>().vcam.m_Lens.LensShift = new Vector3(0,6,0);
        ChangeMusic();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        GameObject.Find("bossRoom").GetComponent<bossRoomController>().positionBeforeBoss = PlayerController.Instance.transform.position;
        MainCanvas.Instance.EnableCanvas(false);
        PlayerController.Instance.sizeHandler.enabled = false;
        PlayerController.Instance.playerWeapon.enabled = false;
        PlayerController.Instance.canControll = false;
        PlayerController.Instance.playerMovement.leftDirection = true;
        PlayerController.Instance._animator.SetBool("Walking", false);
        reimuOutShooter.SetActive(false);
        reimuSprite.SetActive(true);
        
        reimu.GetComponent<ReimuMovement>().enabled = false;
        var reimuBattle = reimu.GetComponent< ReimuBattle>();
        reimuBattle.StopAllCoroutines();
        reimuBattle.chargeBar.SetActive(false);
        reimuBattle.isCharge = false;
        reimuBattle.weapon.SetActive(false);
        if (reimu.GetComponent<ReimuMovement>().ismove)
        {
            yield return StartCoroutine(reimuBattle.SmoothMoveCoroutine(startPosition, endPosition, 2f));
        }
        else
        {
            yield return StartCoroutine(reimuBattle.SmoothMoveCoroutine(reimuSprite.transform.localPosition, endPosition, 2f));
        }

        reimuBattle.CloseAllFeedback();
        PlayerController.Instance.CloseAllFeedbacks();
        DialogManager.Instance.PlayStory();

        yield return new WaitUntil(() => GameObject.Find("bossRoom").GetComponent<bossRoomController>().isReady);

        // 動畫演出

        reimu.GetComponent<ReimuBoss>().beautifulEffect.SetActive(true);
        reimu.GetComponent<Animator>().SetTrigger("HandsUp");

        yield return new WaitForSeconds(3f);

        reimu.GetComponent<ReimuBoss>().beautifulEffect.SetActive(false);
        reimu.GetComponent<Animator>().SetTrigger("Idle");



        PlayerController.Instance.transform.position = bossRoomLocation;
        PlayerController.Instance.sizeHandler.enabled = true;
        MainCanvas.Instance.EnableCanvas(true);
        PlayerController.Instance.sizeHandler.currentSize = 1;
        PlayerController.Instance.canControll = true;
        PlayerController.Instance.playerWeapon.enabled = true;
        reimu.GetComponent<ReimuBattle>().enabled = false;
        reimu.GetComponent<ReimuBoss>().enabled = true;
        reimu.GetComponent<ReimuBoss>().reimuSprite.transform.position = bossRoomLocation + new Vector3(-12.5f, 7f, 0);



    }
}
