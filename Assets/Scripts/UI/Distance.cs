using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Distance : MonoBehaviour
{
    [SerializeField] private ReimuMovement reimuMovement;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private Image ReimuIcon;

    // Start is called before the first frame update
    void Start()
    {
        reimuMovement = GameObject.Find("Reimu").GetComponent<ReimuMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Reimu") == null)
        {
            distanceText.enabled = false;
            ReimuIcon.enabled = false;
            return;
        }
        float distance = reimuMovement.GetDistance();
        if (distance == 0f || GameObject.Find("Reimu").GetComponent<ReimuBoss>().enabled)
        {
            distanceText.enabled = false;
            ReimuIcon.enabled = false;
        } else
        {
            distanceText.enabled = true;
            ReimuIcon.enabled = true;
            distanceText.text = distance.ToString("F0") + "m";
        }
    }
}
