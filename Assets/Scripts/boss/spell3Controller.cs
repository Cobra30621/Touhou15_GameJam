using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spell3Controller : MonoBehaviour
{
    public GameObject[] SpellPosition;
    [SerializeField] private int id = 0;
    [SerializeField] private ReimuBoss reimuboss;
    [SerializeField] private float shootDuration;
    [SerializeField] private float moveDuration;


    public IEnumerator SpellStart()
    {
        reimuboss = GameObject.Find("Reimu").GetComponent<ReimuBoss>();
        StartCoroutine(reimuboss.SmoothMoveCoroutine(reimuboss.reimuSprite.transform.position, SpellPosition[0].transform.position, moveDuration));
        while (true)
        {
            SpellPosition[id].SetActive(true);
            yield return new WaitForSeconds(shootDuration);
            SpellPosition[id].SetActive(false);
            Debug.Log(SpellPosition[id].transform.position);
            Debug.Log(SpellPosition[(id + 1) % 4].transform.position);
            StartCoroutine(reimuboss.SmoothMoveCoroutine(SpellPosition[id].transform.position, SpellPosition[(id + 1) % 4].transform.position, moveDuration));
            id = (id + 1) % 4;
            yield return new WaitForSeconds(moveDuration);

        }
    }
}
