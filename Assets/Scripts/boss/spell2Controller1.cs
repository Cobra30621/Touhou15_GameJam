using Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Weapon;

public class spell2Controller : MonoBehaviour
{
    public GameObject[] SpellPosition;
    [SerializeField] private int id = 0;
    [SerializeField] private ReimuBoss reimuboss;
    [SerializeField] private float shootDuration;
    [SerializeField] private float moveDuration;
    public GameObject shooter;
    public GameObject shooter2;


    public void Disable()
    {
        for (int i = 0; i < SpellPosition.Length; i++)
        {
            SpellPosition[i].SetActive(false);
        }
    }

    public IEnumerator SpellStart()
    {
        reimuboss = GameObject.Find("Reimu").GetComponent<ReimuBoss>();
        while (true)
        {
            Vector3 leftPosition = SpellPosition[0].transform.position;
            yield return StartCoroutine(reimuboss.SmoothMoveCoroutine(reimuboss.reimuSprite.transform.position, leftPosition, 0.5f));
            leftPosition.x += 40f;
            StartCoroutine(reimuboss.SmoothMoveCoroutine(reimuboss.reimuSprite.transform.position, leftPosition, moveDuration));
            shooter.SetActive(true);
            shooter2.SetActive(true);
            yield return new WaitForSeconds(moveDuration);
            shooter.SetActive(false);
            shooter2.SetActive(false);


            leftPosition = SpellPosition[1].transform.position;
            yield return StartCoroutine(reimuboss.SmoothMoveCoroutine(reimuboss.reimuSprite.transform.position, leftPosition, 0.5f));
            leftPosition.x -= 40f;
            StartCoroutine(reimuboss.SmoothMoveCoroutine(reimuboss.reimuSprite.transform.position, leftPosition, moveDuration));
            shooter.SetActive(true);
            shooter2.SetActive(true);
            shooter.GetComponent<EnemyWeapon>().customDirection = new Vector3(-1, 1, 0);
            shooter2.GetComponent<EnemyWeapon>().customDirection = new Vector3(-1, -1, 0);
            yield return new WaitForSeconds(moveDuration);
            shooter.SetActive(false);
            shooter2.SetActive(false);


            leftPosition = SpellPosition[2].transform.position;
            yield return StartCoroutine(reimuboss.SmoothMoveCoroutine(reimuboss.reimuSprite.transform.position, leftPosition, 0.5f));
            leftPosition.x += 40f;
            StartCoroutine(reimuboss.SmoothMoveCoroutine(reimuboss.reimuSprite.transform.position, leftPosition, moveDuration));
            shooter.SetActive(true);
            shooter2.SetActive(true);
            shooter.GetComponent<EnemyWeapon>().customDirection = new Vector3(1, 1, 0);
            shooter2.GetComponent<EnemyWeapon>().customDirection = new Vector3(1, -1, 0);
            yield return new WaitForSeconds(moveDuration);
            shooter.SetActive(false);
            shooter2.SetActive(false);


            leftPosition = SpellPosition[3].transform.position;
            yield return StartCoroutine(reimuboss.SmoothMoveCoroutine(reimuboss.reimuSprite.transform.position, leftPosition, 0.5f));
            yield return new WaitForSeconds(5f);

        }
    }
}
