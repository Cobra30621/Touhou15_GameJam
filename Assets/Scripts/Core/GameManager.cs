﻿using Map.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public int score;
        private float timer;

        public GameMode GameMode;
        
        
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
        
        // 更新分數與計時
        void Update()
        {
            timer += Time.deltaTime;
            // 更新 UI

            //開啟選單
            if (Input.GetKeyDown(KeyCode.Escape))
            {

            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }
        }

        
        [Button]
        public void Restart()
        {
            SceneManager.LoadScene("Game-story");
        }

        public void StartGame()
        {
            // 開始遊戲邏輯
            score = 0;
            timer = 0f;
            // 顯示 UI
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
        }

        [Button]
        public void EnterGoodEnd()
        {
            sceneMannger.ChangeScene(SceneType.GoodEnd);
        }

        [Button]
        public void EnterBadEnd(int score = 0)
        {
            this.score = score;
            if (GameMode == GameMode.Story)sceneMannger.ChangeScene(SceneType.BadEnd);
            else
            {
                sceneMannger.ChangeScene(SceneType.EndInf);
            }
        }

    }
}