using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bosstesttmp : MonoBehaviour
{
    public GameObject reimuSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SmoothMoveCoroutine(Vector3 startPosition, Vector3 endPosition, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            reimuSprite.transform.position = Vector3.Lerp(
                 startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //reimuSprite.transform.position = transform.position + endPosition;
    }
}
