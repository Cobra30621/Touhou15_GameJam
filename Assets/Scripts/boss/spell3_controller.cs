using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spell3_controller : MonoBehaviour
{
    public GameObject[] gameObjects;

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
            gameObjects[((id-1)+4)%4].SetActive(false);
            yield return new WaitForSeconds(gap2);
            gameObjects[id].SetActive(true);
            id++;
            id %= 4;
            yield return new WaitForSeconds(gap1);
            
        }
    }
}
