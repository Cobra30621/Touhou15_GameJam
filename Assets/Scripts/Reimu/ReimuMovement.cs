using Player;
using Sirenix.OdinInspector;
using UnityEngine;

public class ReimuMovement : MonoBehaviour
{
    [SerializeField] private float startDistance = 30;
    
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    [SerializeField] private float playerSpeedFactor;
    [SerializeField] private ReimuBattle reimuBattle;
    // Start is called before the first frame update


    private Animator _animator;

    void Start()
    {
        speed = 0.02f;
        playerSpeedFactor = 0.0004f;

        _animator = GetComponent<Animator>();
        
        StartMode();
    }

    [Button]
    public void StartMode()
    {
        distance = startDistance;
        _animator.SetTrigger("Init");
    }

    // Update is called once per frame
    void Update()
    {
        if (distance > 0)
        {
            distance -= (speed - PlayerController.Instance.playerMovement.GetVelocityX() * playerSpeedFactor);
        }
        else
        {
            distance = 0;
            ActivateReimuBattle();
        }
    }
    
    [Button]
    private void ActivateReimuBattle()
    {
        reimuBattle.StartMode();
    }

    public float GetDistance() {
        return distance;
    }
}
