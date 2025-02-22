using System;
using System.Collections.Generic;
using Core;
using Map.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Map
{
    /// <summary>
    /// Manages the map generation and stage transitions.
    /// </summary>
    public class MapManager : MonoBehaviour
    {
        #region Settings

        /// <summary>
        /// Singleton instance of the MapManager.
        /// </summary>
        public static MapManager Instance { get; private set; }

        /// <summary>
        /// Reference to the MapSpawner responsible for generating rooms.
        /// </summary>
        [FormerlySerializedAs("mapSpawner")] public RoomSpawner roomSpawner;
        
        /// <summary>
        /// Data for the current stage.
        /// </summary>
        [Required]
        [InlineEditor]
        public StageData StageData;
        
        /// <summary>
        /// Data associated with the Room.
        /// </summary>
        [Required]
        [InlineEditor]
        [SerializeField] private RoomData roomData;
        
        /// <summary>
        /// The initial number of rooms to spawn.
        /// </summary>
        [Title("Settings")]
        public int initialRoomCount;

        /// <summary>
        /// The threshold for the spawn interval to generate new rooms.
        /// </summary>
        public int spawnIntervalThreshold = 2;
        
        #endregion

        #region Cache

        /// <summary>
        /// List of current stages in the game.
        /// </summary>
        [Title("Cache")]
        [SerializeField]
        private List<Stage> _currentStages;
        
        /// <summary>
        /// Index of the current stage.
        /// </summary>
        public int currentStageIndex;
        
        /// <summary>
        /// The current stage being played.
        /// </summary>
        public Stage currentStage;


        [SerializeField] private List<Room> roomPrefabs;
        
        /// <summary>
        /// The current spawn room ID from the map spawner.
        /// </summary>
        [ShowInInspector]
        public int currentSpawnRoomId => roomSpawner.currentSpawnId;
        
        /// <summary>
        /// Number of rooms spawned in the current stage.
        /// </summary>
        public int roomsSpawnedInCurrentStage;
        
        /// <summary>
        /// The ID of the character's arrival room in the map.
        /// </summary>
        public int currentCharacterArrivalRoomId;

        /// <summary>
        /// Indicates if the boss has been defeated.
        /// </summary>
        public bool IsDefeatBoss;
        

        #endregion

        #region Initialization


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
            Initialize();
        }

        /// <summary>
        /// Initializes the map and stage settings.
        /// </summary>
        private void Initialize()
        {
            currentCharacterArrivalRoomId = 0;
            roomSpawner.Initialize();

            var gameMode = GameManager.Instance.GameMode;
            _currentStages = StageData.GetStages(gameMode);
            currentStageIndex = 0;
            roomsSpawnedInCurrentStage = 0;
            currentStage = _currentStages[currentStageIndex];

            GenerateInitialRoom();
        }
        

        #endregion

        #region Generate Room

        /// <summary>
        /// Generates the initial map layout.
        /// </summary>
        private void GenerateInitialRoom()
        {
            for (int i = 0; i < initialRoomCount; i++)
            {
                CheckAndGenerateRoom();
            }
        }

        /// <summary>
        /// Checks if the stage should change and generates a new room if necessary.
        /// </summary>
        private void CheckAndGenerateRoom()
        {
            // Check should change stage
            if (ShouldChangeStage())
            {
                currentStageIndex++;
                // If all stage complete, stop generate room
                if (currentStageIndex >= _currentStages.Count)
                {
                    return;
                }
                
                currentStage = _currentStages[currentStageIndex];
                roomsSpawnedInCurrentStage = 0;

                if (IsBossStage(currentStage.StageName))
                {
                    IsDefeatBoss = false;
                }
            }
            
            // Generate Room
            var roomPrefab = GetRandomRoom();
            roomSpawner.GenerateRoom(roomPrefab);
            roomsSpawnedInCurrentStage++;
        }

        /// <summary>
        /// Determines if the stage should change based on the current conditions.
        /// </summary>
        /// <returns>True if the stage should change, otherwise false.</returns>
        private bool ShouldChangeStage()
        {
            // If boss stage, need to defeat boss then change stage
            if (IsBossStage(currentStage.StageName))
            {
                return IsDefeatBoss;
            }
            else
            {
                // Unlimited Generate Room
                if (currentStage.RequiredRoomCount == -1)
                {
                    return false;
                }
                
                return roomsSpawnedInCurrentStage >= currentStage.RequiredRoomCount;
            }
        }

        private bool IsBossStage(StageName stageName)
        {
            return stageName == StageName.BigForestBoss;
        }


        private Room GetRandomRoom()
        {
            CheckRoomPrefabsThenGenerate();
            
            // random _room
            var randomIndex = UnityEngine.Random.Range(0, roomPrefabs.Count);
            var roomPrefab = roomPrefabs[randomIndex];
            roomPrefabs.Remove(roomPrefab);

            return roomPrefab;
        }

        private void CheckRoomPrefabsThenGenerate()
        {
            if (roomPrefabs.Count == 0)
            {
                roomPrefabs = roomData.GetRooms(currentStage.StageName);
            }
        }
        

        #endregion

        /// <summary>
        /// When DefeatBoss
        /// </summary>
        public void DefeatBoss()
        {
            IsDefeatBoss = true;
        }
        
        /// <summary>
        /// Completes a room based on the character's arrival ID.
        /// </summary>
        /// <param name="id">The ID of the room that has been completed.</param>
        public void CompleteRoom(int id)
        {
            currentCharacterArrivalRoomId = Math.Max(id, currentCharacterArrivalRoomId);
            
            // Need Generate new room
            if (currentSpawnRoomId - currentCharacterArrivalRoomId <= spawnIntervalThreshold)
            {
                CheckAndGenerateRoom();
            }
        }
    }
}