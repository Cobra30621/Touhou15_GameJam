using System;
using UnityEngine;

namespace Map
{
    /// <summary>
    /// Represents the goal of a room in the game.
    /// </summary>
    public class RoomGoal : MonoBehaviour
    {
        /// <summary>
        /// The room associated with this goal.
        /// </summary>
        private Room _room;
    
        /// <summary>
        /// Sets the room for this goal.
        /// </summary>
        /// <param name="room">The room to be set.</param>
        public void SetRoom(Room room)
        {
            _room = room;
        }

        private void OnTriggerStay2d(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                CompleteGoal();
            }
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                CompleteGoal();
            }
        }

        private void CompleteGoal()
        {
            _room.CompleteGoal();
                
            Destroy(gameObject);
        }
    }
}