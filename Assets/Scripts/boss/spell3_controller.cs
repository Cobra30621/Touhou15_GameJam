using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spell3_controller : MonoBehaviour
{
    public GameObject[] gameObjects;
    public bosstesttmp boss;
    [SerializeField] float gap1, gap2;
    [SerializeField] int id=0;
    private void Start()
    {
        StartCoroutine(start());
    }

    IEnumerator start()
    {
        while (true)
        {
            gameObjects[id].SetActive(true);
            yield return new WaitForSeconds(gap1);
            gameObjects[id].SetActive(false);
            StartCoroutine(boss.SmoothMoveCoroutine(gameObjects[id].transform.position, gameObjects[(id + 1) % 4].transform.position, gap2));
            id =(id+1)%4;
            yield return new WaitForSeconds(gap2);

        }
    }
}
