using Player;
using UnityEngine;

public class ReimuMovement : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    [SerializeField] private float playerSpeedFactor;
    [SerializeField] private ReimuBattle reimuBattle;
    // Start is called before the first frame update


    [SerializeField]
    private GameObject player;

    private PlayerController playerController;

    void Start()
    {
        distance = 30f;
        speed = 0.02f;
        playerSpeedFactor = 0.0004f;

        player = GameObject.Find("Player");

        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (distance > 0)
        {
            distance -= (speed - playerController.getVelocityX() * playerSpeedFactor);
        }
        else
        {
            distance = 0;
            ActivateReimuBattle();
        }
    }
    private void ActivateReimuBattle()
    {
        if (reimuBattle.enabled == false)
        {
            reimuBattle.enabled = true;
        }
    }

    public float GetDistance() {
        return distance;
    }
}
