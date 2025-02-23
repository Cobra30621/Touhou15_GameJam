using Map;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField] private bool isStoryMode;
    [SerializeField] private GameObject startBackground;
    [SerializeField] private GameObject processBackground;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 nextBackgroundPosition;
    [SerializeField] private float distance;

    // Start is called before the first frame update
    void Start()
    {
        nextBackgroundPosition = startPosition;
        GenerateBackgroundinEndless(startBackground);
        GenerateBackgroundinEndless(processBackground);
        GenerateBackgroundinEndless(processBackground);
    }


    // Update is called once per frame
    void Update()
    {
        if (Player.PlayerController.Instance.transform.position.x > nextBackgroundPosition.x - 30f)
        {
            GenerateBackgroundinEndless(processBackground);
        }
    }

    private void GenerateBackgroundinEndless(GameObject background)
    {
        var room = Instantiate(background, transform);
        room.transform.position = nextBackgroundPosition;
        nextBackgroundPosition.x += distance;
    }
}
