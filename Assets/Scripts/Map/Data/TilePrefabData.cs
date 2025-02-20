using System.Collections.Generic;
using UnityEngine;

namespace Map.Data
{
    /// <summary>
    /// Data associated with the tile prefab.
    /// </summary>
    [CreateAssetMenu(fileName = "Tile Prefab Data", menuName = "Tile Prefab Data")]
    public class TilePrefabData : ScriptableObject
    {
        public List<TilePrefabPair> TilePrefabPairs;
    }
}