using Player;
using Sirenix.OdinInspector;
using UnityEngine;

public class ReimuMovement : MonoBehaviour
{
    [SerializeField] private const float startDistance = 30;
    
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    [SerializeField] private float playerSpeedFactor;
    [SerializeField] private ReimuBattle reimuBattle;
    // Start is called before the first frame update

    public bool isfreeze = false;
    public float speedFactor = 1.0f;

    private Animator _animator;
    public bool ismove = true,shoot_cool = false;
    public float shoot_cool_time = 1f;
    public GameObject shooter;

    void Start()
    {
        speed = 0.02f;
        playerSpeedFactor = 0.0004f;

        _animator = GetComponent<Animator>();
        ismove = true;
        StartMode();
    }

    [Button]
    public void StartMode(float dis = startDistance)
    {
        ismove = true;
        distance = dis;
        _animator.SetTrigger("Init");
    }

    // Update is called once per frame
    void Update()
    {
        if (distance > 0)
        {
            if (!isfreeze) distance -= (speed - PlayerController.Instance.playerMovement.GetVelocityX() * playerSpeedFactor)*speedFactor;
        }
        else
        {
            distance = 0;
            ActivateReimuBattle();
        }

        if(ismove)
        {
            if (!shoot_cool)
            {
                shoot_cool = true;
            }
        }
    }
    
    private void shoot()
    {

    }

    [Button]
    private void ActivateReimuBattle()
    {
        ismove = false;
        reimuBattle.StartMode();
    }

    public float GetDistance() {
        return distance;
    }
}
