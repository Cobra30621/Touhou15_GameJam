using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Map.Data
{
    /// <summary>
    /// Represents the data for a room in the game
    /// </summary>
    [CreateAssetMenu(fileName = "Room Data", menuName = "Room Data")]
    public class RoomData : SerializedScriptableObject
    {
        [Required]
        public Room defaultRoom;

        [SerializeField]
        private Dictionary<StageName, List<Room>> _stageRooms;

        
        /// <summary>
        /// Returns a random room from the list of rooms.
        /// </summary>
        /// <returns>A randomly selected Room object.</returns>
        public Room RandomRoom(StageName stageName)
        {
            if (_stageRooms.ContainsKey(stageName))
            {
                var rooms = _stageRooms[stageName];
                if (rooms.Count != 0) 
                    return rooms[Random.Range(0, rooms.Count)];
                
                Debug.LogError($"No room found in {stageName} stage in RoomData, please set data");
            }
            else
            {
                Debug.LogError($"Unknown stage {stageName} in RoomData, please set data");
            }

            return defaultRoom;
        }
        
        
        
    }
}