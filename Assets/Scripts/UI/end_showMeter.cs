using Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class end_showMeter : MonoBehaviour
{
   public TextMeshProUGUI scoreText;
    private void Start()
    {
        scoreText.text =GameManager.Instance.score+"  M!";
    }
}
