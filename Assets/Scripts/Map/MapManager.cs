using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        /// <summary>
        /// Singleton instance of the MapManager.
        /// </summary>
        public static MapManager Instance { get; private set; }

        /// <summary>
        /// Reference to the MapSpawner responsible for generating rooms.
        /// </summary>
        public MapSpawner mapSpawner;
    
        /// <summary>
        /// The ID of the character's arrival in the map.
        /// </summary>
        public int currentCharacterArrivalId;

        /// <summary>
        /// The initial number of rooms to spawn.
        /// </summary>
        public int initSpawnCount;

        /// <summary>
        /// The threshold for the spawn interval to generate new rooms.
        /// </summary>
        public int spawnIntervalThreshold = 2;
        
        /// <summary>
        /// Initializes the MapManager instance.
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        
        /// <summary>
        /// Starts the map generation process.
        /// </summary>
        private void Start()
        {
            GenerateInitMap();
        }

        
        /// <summary>
        /// Generates the initial map layout.
        /// </summary>
        private void GenerateInitMap()
        {
            currentCharacterArrivalId = 0;
            mapSpawner.Initialize();
            
            mapSpawner.GenerateStartingRoom();
            
            for (int i = 0; i < initSpawnCount; i++)
            {
                mapSpawner.GenerateRandomRoom();
            }
        }
        
        /// <summary>
        /// Completes a room based on the character's arrival ID.
        /// </summary>
        /// <param name="id">The ID of the room that has been completed.</param>
        public void CompleteRoom(int id)
        {
            currentCharacterArrivalId = Math.Max(id, currentCharacterArrivalId);
         
            
            if (mapSpawner.currentSpawnId - currentCharacterArrivalId <= spawnIntervalThreshold)
            {
                mapSpawner.GenerateRandomRoom();
            }
        }
    }
}