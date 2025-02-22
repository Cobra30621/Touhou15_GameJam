using Map;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image progressBar; // 指定 UI Image（前景條）
    public float progress = 0f; // 進度值（0 ~ 1）
    public MapManager mapManager;
    public float all_room, now_room;
    private void Start()
    {
        var dict = mapManager.StageData.stageDict[Map.Data.GameMode.Story];
        foreach (var item in dict)
        {
            all_room += item.RequiredRoomCount;
            if(item.RequiredRoomCount == -1)
            {
                ErrorManager.Error("RequiredRoomCount is -1");
            }
        }
    }
    void Update()
    {
        SetProgress((float)mapManager.nowRoomCount/all_room);
        progressBar.fillAmount = Mathf.Clamp01(progress);
        // 設定進度條的填充程度
        
    }


    public void SetProgress(float value)
    {
        progress = Mathf.Clamp01(value);
    }
}
