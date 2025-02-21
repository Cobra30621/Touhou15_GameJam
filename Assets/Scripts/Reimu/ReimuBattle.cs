using Player;
using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Weapon;

public class ReimuBattle : MonoBehaviour
{
    [SerializeField] private GameObject reimuSprite;
    [SerializeField] private GameObject reimu;
    [SerializeField] private float movePeriod;
    [SerializeField] private float chargePeriod;
    [SerializeField] private float dashPeriod;
    [SerializeField] private bool isMove = false;

    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private GameObject chargeBar;
    [SerializeField] private GameObject weapon;

    private ReimuMovement _reimuMovement;

    
    private Animator _animator;

    public bool IsRunning;
    
    private bool isHit;

    private Coroutine actionCoroutine;
    

    void Start()
    {
        _reimuMovement = GetComponent<ReimuMovement>();
        _animator = GetComponent<Animator>();
    }


    public void StartMode()
    {
        if (IsRunning)
        {
            return;
        }
        
        IsRunning = true;
        reimuSprite.SetActive(true);
        actionCoroutine = StartCoroutine(ReimuActionCoroutine());
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


    private IEnumerator ReimuActionCoroutine()
    {
        weapon.SetActive(true);
        yield return StartCoroutine(SmoothMoveCoroutine(startPosition, endPosition, movePeriod));
        weapon.SetActive(false);
        yield return StartCoroutine(ChargeAttack());
        chargeBar.SetActive(false);
        PlayerController.Instance.Die();
        yield return StartCoroutine(SmoothMoveCoroutine(endPosition, 
            reimu.transform.InverseTransformPoint(PlayerController.Instance.transform.position), dashPeriod));
    }


    private IEnumerator SmoothMoveCoroutine(Vector3 startPosition, Vector3 endPosition, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            reimuSprite.transform.position =  Vector3.Lerp( 
                transform.position + startPosition, transform.position + endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        reimuSprite.transform.position =  transform.position + endPosition;
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

    
    [Button]
    public void OnHit()
    {
        StartCoroutine(OnHitCoroutine());
    }

    private IEnumerator OnHitCoroutine()
    {
        StopCoroutine(actionCoroutine);
        chargeBar.SetActive(false);
        weapon.SetActive(false);
        _animator.SetTrigger("Dizziness");
        
        yield return new WaitForSeconds(1f);
        
        // 平移 reimuSprite 出畫面外
        Vector3 targetPosition = new Vector3(-15f, reimuSprite.transform.position.y, reimuSprite.transform.position.z); // 假設 -10f 是畫面外的位置
        yield return StartCoroutine(SmoothMoveCoroutine(reimuSprite.transform.localPosition, targetPosition, 2f)); // 2 秒的平移時間
        
        reimuSprite.SetActive(false);
        // End Battle Mode
        _reimuMovement.StartMode();
        IsRunning = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue; // 設定顏色為紅色
        Gizmos.DrawSphere(Camera.main.transform.position + startPosition, 0.1f); // 繪製 startPosition 的球體
        Gizmos.color = Color.green; // 設定顏色為綠色
        Gizmos.DrawSphere(Camera.main.transform.position + endPosition, 0.1f); // 繪製 endPosition 的球體
    }
}
