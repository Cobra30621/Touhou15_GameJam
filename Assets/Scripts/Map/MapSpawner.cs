using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map
{
    public class MapSpawner : MonoBehaviour
    {
        [Required]
        [SerializeField] private RoomData roomData;
        
        public int spawnCount;

        [SerializeField]
        private int currentX; 

        [SerializeField] private List<Room> currentRooms;
        
        
        [Button]
        public void Init()
        {
            currentX = 0;
            RemoveAllRooms();
        }
        
        [Button]
        public void GenerateMap()
        {
            GenerateRoom(roomData.startRoom);
            
            for (int i = 0; i < spawnCount; i++)
            {
                Room roomPrefab = roomData.RandomRoom();
                GenerateRoom(roomPrefab);
            }
            
            GenerateRoom(roomData.endRoom);
        }

        private void GenerateRoom(Room roomPrefab)
        {
            BoundsInt bounds = roomPrefab.GetBoundX();
            var room = Instantiate(roomPrefab, transform);
            room.transform.position = new Vector3( currentX - bounds.xMin, 0, 0);
            currentRooms.Add(room);

            currentX += bounds.size.x;
        }

        public void RemoveFirstRoom()
        {
            if (currentRooms.Count > 0)
            {
                Destroy(currentRooms[0].gameObject); 
                currentRooms.RemoveAt(0); 
            }
        }

        public void RemoveAllRooms()
        {
            foreach (var room in currentRooms)
            {
                Destroy(room.gameObject); 
            }
            currentRooms.Clear();
        }
    }
}