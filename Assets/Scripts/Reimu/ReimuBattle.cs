using Player;
using System;
using System.Collections;
using UnityEngine;

public class ReimuBattle : MonoBehaviour
{
    [SerializeField] private GameObject reimuSprite;
    [SerializeField] private GameObject reimu;
    [SerializeField] private float movePeriod;
    [SerializeField] private float chargePeriod;
    [SerializeField] private float dashPeriod;
    [SerializeField] private bool isMove = false;
    [SerializeField] private Vector3 move;
    [SerializeField] private Vector3 initPosition;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private GameObject chargeBar;

    private GameObject player;
    private PlayerController playerController;



    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        reimuSprite.SetActive(true);
        StartCoroutine(ReimuActionCoroutine());
    }

    void FollowMainCamera()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            Vector3 targetPosition = mainCamera.transform.position;
            targetPosition.y = 0;
            targetPosition.z = 0;
            reimu.transform.position = targetPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FollowMainCamera();
    }

    public void ReimuAction()
    {
        StartCoroutine(ReimuActionCoroutine());
    }

    private IEnumerator ReimuActionCoroutine()
    {
        yield return StartCoroutine(SmoothMoveCoroutine(startPosition, endPosition, movePeriod));
        yield return StartCoroutine(ChargeAttack());
        chargeBar.SetActive(false);
        playerController.Die();
        yield return StartCoroutine(SmoothMoveCoroutine(endPosition, reimu.transform.InverseTransformPoint(player.transform.position), dashPeriod));
    }


    private IEnumerator SmoothMoveCoroutine(Vector3 startPosition, Vector3 endPosition, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            reimuSprite.transform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        reimuSprite.transform.localPosition = endPosition;
    }

    private IEnumerator ChargeAttack()
    {
        chargeBar.SetActive(true);
        Vector3 initialScale = new Vector3(0, chargeBar.transform.localScale.y, chargeBar.transform.localScale.z);
        Vector3 targetScale = new Vector3(0.5f, initialScale.y, initialScale.z);
        float elapsedTime = 0;

        while (elapsedTime < chargePeriod)
        {
            chargeBar.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / chargePeriod);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        chargeBar.transform.localScale = targetScale;
    }
}
