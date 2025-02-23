using System;
using Feedback;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    /// <summary>
    /// Controls the player's size
    /// </summary>
    public class PlayerSizeHandler : MonoBehaviour {
        [Header("設定")]
        [SerializeField] private float minSize = 1f; 
        [SerializeField] private float maxSize = 10.0f; 
        [SerializeField] private float[] growthRatelist = new float[] {0,1 };
        [SerializeField] private float[] growthbond = new float[] { 0,1};
        [SerializeField] private float growthRate;
        [SerializeField] private float stun_time = 0.5f;

        [Header("參數")]
        [SerializeField]
        [LabelText("currentSize (0~1)")]
        public float currentSize = 1f;
        
        private float growthTimer = 0f; // 添加计时器变量

        public float smallInterval = 2f;
        public bool sizefreeze = false;
        
        [SerializeField] private ParticleFeedback smallerFeedback;

        private void Update() {
            GrowOverTime();
            UpdateSize();
        }

        [Button]
        public void Resize(float amount)
        {
            Debug.Log(amount);
            currentSize += amount;
            if (currentSize <= 0)
            {
                currentSize = 0;
                StartCoroutine(PlayerController.Instance.stun(stun_time));
            }
        }


        /// <summary>
        /// Increases size over time
        /// </summary>
        private void GrowOverTime(){
            if(sizefreeze){return;}
            
            growthTimer += Time.deltaTime;
            if (growthTimer >= smallInterval)
            {
                check_growthRate();    
                currentSize += growthRate * smallInterval; 
                smallerFeedback.Play(transform, true);
                growthTimer = 0f; // 重置计时器
            }
        }
        
        private void check_growthRate()
        {
            for (int i = 0; i < growthbond.Length; i++)
            {
                if (currentSize >= growthbond[i])
                {
                    growthRate = growthRatelist[i];
                }
            }
        }
        
        /// <summary>
        /// Updates the GameObject size
        /// </summary>
        private void UpdateSize()
        {
            currentSize = Math.Clamp(currentSize, 0f, 1f);
            var objectSize = minSize + (maxSize - minSize) * currentSize;

            var xSize = PlayerController.Instance.LeftDirection ? -1 * objectSize : objectSize;
            transform.localScale = new Vector3(xSize, objectSize, objectSize); // 应用大小
        }

    }
}