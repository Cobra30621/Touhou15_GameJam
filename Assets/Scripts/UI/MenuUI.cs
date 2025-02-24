using UnityEngine;

namespace UI
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject rulePanel;
        

        public void StartGame()
        {
            sceneMannger.ChangeScene(SceneType.SelectMode);
        }
        
        public void QuitGame()
        {
            Application.Quit();
        }

        public void OpenRulePanel()
        {
            rulePanel.SetActive(true);
        }
        
        
        
    }
}