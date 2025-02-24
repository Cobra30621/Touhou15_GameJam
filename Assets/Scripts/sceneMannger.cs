using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneType
{
    StartMenu,
    SelectMode,
    Game_inf,
    Game_story,
    BadEnd,
    GoodEnd,
    EndInf
}
public class sceneMannger : MonoBehaviour
{
    private static Dictionary<SceneType, string> sceneDic = new Dictionary<SceneType, string>()
    {   
        {SceneType.StartMenu, "menu0" },
        {SceneType.SelectMode, "menu" },
        {SceneType.Game_inf, "Game-inf" },
        { SceneType.Game_story, "Game-story" },
        {   SceneType.BadEnd, "Bad-end"},
        {   SceneType.GoodEnd, "Good-end"},
        { SceneType.EndInf,"End-inf"}
    };
    public static void ChangeScene(SceneType sceneType)
    {
        string sceneName = sceneDic[sceneType];
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    public static void ChangeScene(string sceneType)
    {
        if(sceneType == "Game-inf")
        {
            GameManager.Instance.GameMode = Map.Data.GameMode.Endless;
        }
        else if (sceneType == "Game-story")
        {
            GameManager.Instance.GameMode = Map.Data.GameMode.Story;
        }
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneType);
    }
}
