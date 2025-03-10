﻿using System;
using System.Collections.Generic;
using Map.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Map
{
    public class RoomSpawner : MonoBehaviour
    {
        /// <summary>
        /// Data associated with the Room.
        /// </summary>
        [Required]
        [InlineEditor]
        [SerializeField] private RoomData roomData;


        public float yOffset = -12f;
        
        /// <summary>
        /// Current spawn ID for rooms.
        /// </summary>
        public int currentSpawnId;

        /// <summary>
        /// Current position on the X-axis for room placement.
        /// </summary>
        [SerializeField]
        private int currentX; 

        /// <summary>
        /// List of currently spawned rooms.
        /// </summary>
        [SerializeField] private List<Room> activeRooms;

        /// <summary>
        /// Initializes the map spawner by resetting spawn ID and position.
        /// </summary>
        [Button]
        public void Initialize()
        {
            currentSpawnId = 0;
            currentX = -10;
            ClearAllRooms();
        }
        


        

        /// <summary>
        /// Instantiates a room prefab and adds it to the active rooms list.
        /// </summary>
        /// <param name="roomPrefab">The room prefab to instantiate.</param>
        public void GenerateRoom(Room roomPrefab)
        {
            BoundsInt bounds = roomPrefab.GetFilledBounds();
            currentSpawnId++;
            
            var room = Instantiate(roomPrefab, transform);
            room.transform.position = new Vector3(currentX - bounds.xMin, yOffset - bounds.yMin, 0);
            room.Initialize(currentSpawnId);
            
            activeRooms.Add(room);
            currentX += bounds.size.x;
        }

        /// <summary>
        /// Removes the first room from the active rooms list.
        /// </summary>
        public void RemoveFirstRoom()
        {
            if (activeRooms.Count > 0)
            {
                Destroy(activeRooms[0].gameObject); 
                activeRooms.RemoveAt(0); 
            }
        }

        /// <summary>
        /// Clears all rooms from the active rooms list.
        /// </summary>
        public void ClearAllRooms()
        {
            foreach (var room in activeRooms)
            {
                Destroy(room.gameObject); 
            }
            activeRooms.Clear();
        }
    }
}