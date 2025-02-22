using Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class inf_meter : MonoBehaviour
{

    public TextMeshProUGUI uiText;

    private void Update()
    {
        uiText.text = (int)(PlayerController.Instance.transform.position.x) + " M";
    }
}
