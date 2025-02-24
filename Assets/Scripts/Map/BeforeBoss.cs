using Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeforeBoss : MonoBehaviour
{
    public GameObject reimu;
    public GameObject reimuOutShooter;
    public GameObject reimuSprite;
    public Vector3 startPosition;
    public Vector3 endPosition;

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
        GameObject.FindObjectOfType<Canvas>().enabled = false;
        PlayerController.Instance.sizeHandler.enabled = false;
        PlayerController.Instance.canControll = false;
        PlayerController.Instance.playerMovement.leftDirection = true;
        PlayerController.Instance._animator.SetBool("Walking", false);
        reimuOutShooter.SetActive(false);
        reimuSprite.SetActive(true);
        reimu.GetComponent<ReimuMovement>().enabled = false;
        reimu.GetComponent<ReimuBattle>().StopAllCoroutines();
        reimu.GetComponent<ReimuBattle>().chargeBar.SetActive(false);
        reimu.GetComponent<ReimuBattle>().isCharge = false;
        reimu.GetComponent<ReimuBattle>().weapon.SetActive(false);
        if (reimu.GetComponent<ReimuMovement>().ismove) {
            StartCoroutine(reimu.GetComponent<ReimuBattle>().SmoothMoveCoroutine(startPosition, endPosition, 2f));
        }
        else
        {
            StartCoroutine(reimu.GetComponent<ReimuBattle>().SmoothMoveCoroutine(reimuSprite.transform.localPosition, endPosition, 2f));
        }

        // 對話框

        // 靈夢演出

    }
}
