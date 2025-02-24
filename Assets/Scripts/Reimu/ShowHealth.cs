using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowHealth : MonoBehaviour
{
    public GameObject[] healthBar;
    public ReimuBoss reimuBoss;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    // Start is called before the first frame update

    public void UpdateHealth(int HP)
    {
        for (int i = 0; i < healthBar.Length; i++)
        {
            if (i < HP)
            {
                healthBar[i].GetComponent<SpriteRenderer>().sprite = fullHeart;
            }
            else
            {
                healthBar[i].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            }
        }

    }
}
