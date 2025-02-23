using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReimuOutShooter : MonoBehaviour
{

    [SerializeField] private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = PlayerController.Instance.transform.position + offset;
    }
}
