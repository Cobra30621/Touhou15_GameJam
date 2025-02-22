using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Distance : MonoBehaviour
{
    [SerializeField] private ReimuMovement reimuMovement;
    [SerializeField] private TextMeshProUGUI distanceText;

    // Start is called before the first frame update
    void Start()
    {
        reimuMovement = GameObject.Find("Reimu").GetComponent<ReimuMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = reimuMovement.GetDistance();
        distanceText.text = "Distance: " + distance.ToString("F0") + "m";
    }
}
