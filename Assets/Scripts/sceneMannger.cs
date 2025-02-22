using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneType
{
    MainMenu,
    Game_inf,
    Game_stroy,
    GameOver
}
public class sceneMannger : MonoBehaviour
{
    Dictionary<SceneType, string> sceneDic = new Dictionary<SceneType, string>()
    {   {SceneType.MainMenu, "menu" },
        {SceneType.Game_inf, "Game-inf" },
        { SceneType.Game_stroy, "Game-stroy" },
        {   SceneType.GameOver, "end"}
    };
    public void ChangeScene(SceneType sceneType)
    {
        string sceneName = sceneDic[sceneType];
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    public void ChangeScene(string sceneType)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneType);
    }
}
