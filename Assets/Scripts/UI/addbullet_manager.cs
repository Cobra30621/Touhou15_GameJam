using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addbullet_manager : MonoBehaviour
{
    public GameObject floatingUIPrefab; // 指向 FloatingUI 預製物
    public Transform uiCanvas; // 指定 UI Canvas 作為父物件

    public void CreateFloatingUI(Sprite sprite)
    {
        // **將世界座標轉換為 UI 座標**
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        // **生成 UI 物件**
        GameObject newUI = Instantiate(floatingUIPrefab, uiCanvas);


        print(newUI.transform.position+"be");
        //newUI.transform.position = screenPosition;
        print(newUI.transform.position+ "af");

        // **設定 UI 內容**
        addbullet_effect floatingUI = newUI.GetComponent<addbullet_effect>();
        floatingUI.Setup(sprite);
    }
}
