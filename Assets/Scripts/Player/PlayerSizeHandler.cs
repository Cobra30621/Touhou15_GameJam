using System;
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

        [Header("參數")]
        [SerializeField]
        [LabelText("currentSize (0~1)")]
        public float currentSize = 1f;
        
        
        private void Update() {
            GrowOverTime();
            UpdateSize();
        }

        [Button]
        public void Resize(float amount)
        {
            Debug.Log(amount);
            currentSize += amount;
        }

        /// <summary>
        /// Increases size over time
        /// </summary>
        private void GrowOverTime(){
             check_growthRate();    
            currentSize += growthRate * Time.deltaTime; // 根据速率增加大小
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