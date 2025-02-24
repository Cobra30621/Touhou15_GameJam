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

    // Start is called before the first frame update
    void Start()
    {
        reimu = GameObject.Find("Reimu");
        reimuOutShooter = reimu.transform.Find("OutShooter").gameObject;
        reimuSprite = reimu.transform.Find("ReimuSprite").gameObject;

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            BeforeBossShow();
        }
    }

    void BeforeBossShow()
    {
        MainCanvas.Instance.EnableCanvas(false);
        PlayerController.Instance.sizeHandler.enabled = false;
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
            StartCoroutine(reimuBattle.SmoothMoveCoroutine(startPosition, endPosition, 2f));
        }
        else
        {
            StartCoroutine(reimuBattle.SmoothMoveCoroutine(reimuSprite.transform.localPosition, endPosition, 2f));
        }

        // ��ܮ�

        // �F�ںt�X

        PlayerController.Instance.transform.position = bossRoomLocation;
        PlayerController.Instance.sizeHandler.currentSize = 1;
        PlayerController.Instance.canControll = true;
        reimu.GetComponent<ReimuBoss>().enabled = true;

        reimuBattle.CloseAllFeedback();
        PlayerController.Instance.CloseAllFeedbacks();
        DialogManager.Instance.PlayStory();


    }
}
