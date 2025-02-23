using Core;
using Map.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
    public class EndPanel : MonoBehaviour
    {
        [Button]
        public void TryAgain()
        {
            var gameMode = GameManager.Instance.GameMode;

            if (gameMode == GameMode.Endless)
            {
                sceneMannger.ChangeScene(SceneType.Game_inf);
            }
            else
            {
                sceneMannger.ChangeScene(SceneType.Game_story);
            }
        }

        [Button]
        public void BackToMenu()
        {
            sceneMannger.ChangeScene(SceneType.StartMenu);
        }
    }
}