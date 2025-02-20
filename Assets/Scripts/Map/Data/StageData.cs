using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Map.Data
{
    [CreateAssetMenu(fileName = "Stage Data", menuName = "Stage Data")]
    public class StageData : SerializedScriptableObject
    {

        public Dictionary<GameMode, List<Stage>> stageDict;
        
        
        public List<Stage> GetStages(GameMode mode)
        {
            if (stageDict.ContainsKey(mode))
            {
                return stageDict[mode];
            }
            else
            {
                Debug.LogError($"No data found with {mode} mode in StageData, please set data");
                return new List<Stage>();
            }
        }
    }
}