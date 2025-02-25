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

    public void Disable()
    {
        for (int i= 0; i < SpellPosition.Length; i++)
        {
            SpellPosition[i].SetActive(false);
        }
    }
    public IEnumerator SpellStart()
    {
        reimuboss = GameObject.Find("Reimu").GetComponent<ReimuBoss>();
        yield return StartCoroutine(reimuboss.SmoothMoveCoroutine(reimuboss.reimuSprite.transform.position, SpellPosition[0].transform.position, moveDuration));
        while (true)
        {
            reimuboss.GetComponent<Animator>().SetTrigger("HandsUp");
            SpellPosition[id].SetActive(true);
            yield return new WaitForSeconds(shootDuration);
            SpellPosition[id].SetActive(false);
            reimuboss.GetComponent<Animator>().SetTrigger("Idle");
            Debug.Log(SpellPosition[id].transform.position);
            Debug.Log(SpellPosition[(id + 1) % 4].transform.position);
            StartCoroutine(reimuboss.SmoothMoveCoroutine(SpellPosition[id].transform.position, SpellPosition[(id + 1) % 4].transform.position, moveDuration));
            id = (id + 1) % 4;
            yield return new WaitForSeconds(moveDuration);

        }
    }
}
