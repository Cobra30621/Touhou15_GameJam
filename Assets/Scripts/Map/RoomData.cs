using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    /// <summary>
    /// Represents the data for a room in the game, including the start room, normal rooms, and the end room.
    /// </summary>
    public class RoomData : ScriptableObject
    {
        /// <summary>
        /// The starting room of the game.
        /// </summary>
        public Room startRoom;

        /// <summary>
        /// A list of normal rooms that can be randomly selected.
        /// </summary>
        public List<Room> normalRooms;
        

        /// <summary>
        /// Returns a random room from the list of normal rooms.
        /// </summary>
        /// <returns>A randomly selected Room object.</returns>
        public Room RandomRoom()
        {
            return normalRooms[Random.Range(0, normalRooms.Count)];
        }
    }
}