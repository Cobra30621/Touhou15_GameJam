using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    [CreateAssetMenu(fileName = "Room Data", menuName = "RoomData")]
    public class RoomData : ScriptableObject
    {
        public Room startRoom;
        public List<Room> normalRooms;
        public Room endRoom;

        public Room RandomRoom()
        {
            return normalRooms[Random.Range(0, normalRooms.Count)];
        }
    }
}