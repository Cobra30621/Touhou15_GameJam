using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        private int score;
        private float timer;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        void Start()
        {
            // 初始化遊戲狀態
            StartGame();
        }

        public void StartGame()
        {
            // 開始遊戲邏輯
            score = 0;
            timer = 0f;
            // 顯示 UI
        }

        public void EndGame()
        {
            // 結束遊戲邏輯
            // 顯示結束畫面
        }

        // 更新分數與計時
        void Update()
        {
            timer += Time.deltaTime;
            // 更新 UI

            //開啟選單
            if (Input.GetKeyDown(KeyCode.Escape))
            {

            }
        }
    }
}