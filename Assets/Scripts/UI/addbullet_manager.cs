using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addbullet_manager : MonoBehaviour
{
    public GameObject floatingUIPrefab; // ���V FloatingUI �w�s��
    public Transform uiCanvas; // ���w UI Canvas �@��������

    public void CreateFloatingUI(Sprite sprite)
    {
        // **�N�@�ɮy���ഫ�� UI �y��**
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        // **�ͦ� UI ����**
        GameObject newUI = Instantiate(floatingUIPrefab, uiCanvas);


        print(newUI.transform.position+"be");
        //newUI.transform.position = screenPosition;
        print(newUI.transform.position+ "af");

        // **�]�w UI ���e**
        addbullet_effect floatingUI = newUI.GetComponent<addbullet_effect>();
        floatingUI.Setup(sprite);
    }
}
