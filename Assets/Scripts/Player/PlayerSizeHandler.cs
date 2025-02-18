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
        public float minSize = 1f; 
        public float maxSize = 10.0f; 
        public float growthRate = 0.01f;
        
        [Required]
        [SerializeField] 
        private GameObject mainObject;
        
        [Header("參數")]
        [SerializeField]
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
            mainObject.transform.localScale = new Vector3(objectSize, objectSize, objectSize); // 应用大小
        }

    }
}