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
        [SerializeField] private float growthRate = 0.01f;
        
        [Header("參數")]
        [SerializeField]
        [LabelText("currentSize (0~1)")]
        private float currentSize = 0f;
        
        
        private void Update() {
            
            GrowOverTime();
            UpdateSize();
        }

        [Button]
        public void Shrink(float shrink)
        {
            currentSize -= shrink;
        }

        public void Grow(float grow)
        {
            currentSize += grow;
        }

        /// <summary>
        /// Increases size over time
        /// </summary>
        private void GrowOverTime(){
             currentSize += growthRate * Time.deltaTime; // 根据速率增加大小
        }
        
        
        /// <summary>
        /// Updates the GameObject size
        /// </summary>
        private void UpdateSize()
        {
            currentSize = Math.Clamp(currentSize, 0f, 1f);
            var objectSize = minSize + (maxSize - minSize) * currentSize;
            transform.localScale = new Vector3(objectSize, objectSize, objectSize); // 应用大小
        }

    }
}