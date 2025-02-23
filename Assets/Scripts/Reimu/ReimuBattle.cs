using Player;
using System;
using System.Collections;
using Feedback;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Weapon;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Map;

public class ReimuBattle : MonoBehaviour
{
    [SerializeField] public GameObject reimuSprite;
    [SerializeField] private GameObject reimu;
    [SerializeField] private float movePeriod;
    [SerializeField] private float chargePeriod;
    [SerializeField] private float dashPeriod;
    [SerializeField] private bool isMove = false;

    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private GameObject chargeBar;
    [SerializeField] private GameObject weapon;
    [SerializeField] private BoxCollider2D reimuCollider;

    private ReimuMovement _reimuMovement;

    
    private Animator _animator;

    public bool IsRunning;
    
    public bool isHit = false;

    private bool isCharge = false;

    private Coroutine actionCoroutine;

    [Required]
    [SerializeField] private ParticleFeedback defeatFeedback;
    
    [Required]
    [SerializeField] private ParticleSystem chargingFeedback;
    
    [SerializeField] private int shield = 0,shield_distance = 0;

    [SerializeField] private GameObject shield_sprite;

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
        if(MapManager.Instance.nowRoomCount >= shield_distance)
        {
            shield = 1;
            shield_sprite.SetActive(true);
        }
        IsRunning = true;
        reimuSprite.SetActive(true);
        actionCoroutine = StartCoroutine(ReimuActionCoroutine());
    }
    float targety;
    void FollowMainCamera()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            Vector3 targetPosition = mainCamera.transform.position;
            if(isCharge) targetPosition.y = targety;
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
        PlayChargingFeedback(false);
        weapon.SetActive(true);
        print("move");
        yield return StartCoroutine(SmoothMoveCoroutine(startPosition, endPosition, movePeriod));
        weapon.SetActive(false);
        print("Charge Attack");
        yield return StartCoroutine(ChargeAttack());
        reimuCollider.enabled = false;
        chargeBar.SetActive(false);
        
        var playerPos = reimu.transform.InverseTransformPoint(PlayerController.Instance.transform.position);
        yield return StartCoroutine(SmoothMoveCoroutine(endPosition, playerPos
            , dashPeriod));
        
        PlayerController.Instance.Die();
        yield return StartCoroutine(SmoothMoveCoroutine(reimu.transform.InverseTransformPoint(PlayerController.Instance.transform.position),
            playerPos+ new Vector3(5f,0f,0f), dashPeriod));
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
        isCharge = true;
        targety = Camera.main.transform.position.y;
        PlayChargingFeedback(true);
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

    
    //if shield cause error call egg
    [Button]
    public void OnHit()
    {
        if (shield<0)ErrorManager.Error("shield is negative");
        if (shield==0)StartCoroutine(OnHitCoroutine());
        if(shield > 0)
        {
            shield--;
            if(shield == 0)
            {
                shield_sprite.SetActive(false);
            }
        }
    }

    private IEnumerator OnHitCoroutine(float dis = 40)
    {
        isHit = true;
        StopCoroutine(actionCoroutine);
        chargeBar.SetActive(false);
        weapon.SetActive(false);
        _animator.SetTrigger("Dizziness");
        
        defeatFeedback.Play(reimuSprite.transform, true);
        PlayChargingFeedback(false);

        yield return new WaitForSeconds(1f);
        
        // 平移 reimuSprite 出畫面外
        Vector3 targetPosition = new Vector3(-15f, reimuSprite.transform.position.y, reimuSprite.transform.position.z); // 假設 -10f 是畫面外的位置
        yield return StartCoroutine(SmoothMoveCoroutine(reimuSprite.transform.localPosition, targetPosition, 2f)); // 2 秒的平移時間
        
        reimuSprite.SetActive(false);
        // End Battle Mode
        _reimuMovement.StartMode(dis);
        isCharge = false;
        IsRunning = false;
        isHit = false;
    }

    
    [Button]
    private void PlayChargingFeedback(bool isCharge)
    {
        chargingFeedback.gameObject.SetActive(isCharge);
        if (isCharge)
        {
            chargingFeedback.Play();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue; // 設定顏色為紅色
        Gizmos.DrawSphere(Camera.main.transform.position + startPosition, 0.1f); // 繪製 startPosition 的球體
        Gizmos.color = Color.green; // 設定顏色為綠色
        Gizmos.DrawSphere(Camera.main.transform.position + endPosition, 0.1f); // 繪製 endPosition 的球體
    }

    public void stopReimuAttack()
    {
        isCharge = false;
        StopCoroutine(actionCoroutine);
        chargeBar.SetActive(false);
        weapon.SetActive(false);
        _animator.SetTrigger("Dizziness");
    }
    public void startattack()
    {
        _animator.SetTrigger("Init"); 
        StartCoroutine(count());
    }
    IEnumerator count()
    {
        yield return new WaitForSeconds(0.2f);
        reimuSprite.SetActive(true);
        StartCoroutine(ReimuActionCoroutine());
    }

}
