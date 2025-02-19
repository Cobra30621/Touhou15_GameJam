using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Map
{
    public class Room : MonoBehaviour
    {
        /// <summary>
        /// The Tilemap associated with this room.
        /// </summary>
        [Required]
        [SerializeField]
        private Tilemap tilemap;

        /// <summary>
        /// The goal for this room.
        /// </summary>
        [Required]
        [SerializeField] 
        private RoomGoal roomGoal;

        /// <summary>
        /// The unique identifier for this room.
        /// </summary>
        [SerializeField] 
        private int roomId;
        
        /// <summary>
        /// Initializes the room with a given ID.
        /// </summary>
        /// <param name="id">The ID to initialize the room with.</param>
        public void Initialize(int id)
        {
            roomId = id;
            roomGoal.SetRoom(this);
        }
        
        /// <summary>
        /// Marks the room as completed.
        /// </summary>
        public void CompleteGoal()
        {
            MapManager.Instance.CompleteRoom(roomId);
        }
        
        /// <summary>
        /// Gets the bounds of the filled tiles in the Tilemap.
        /// </summary>
        /// <returns>The bounds of the filled tiles.</returns>
        [Button]
        public BoundsInt GetFilledBounds()
        {
            BoundsInt filledBounds = CalculateFilledTileBounds();
            return filledBounds;
        }
        
        /// <summary>
        /// Calculates the bounds of the filled tiles in the Tilemap.
        /// </summary>
        /// <returns>The bounds of the filled tiles.</returns>
        private BoundsInt CalculateFilledTileBounds()
        {
            if (tilemap == null) return new BoundsInt();

            BoundsInt bounds = tilemap.cellBounds;
            int minX = bounds.xMax, minY = bounds.yMax; // Set to maximum value to find the minimum value
            int maxX = bounds.xMin, maxY = bounds.yMin; // Set to minimum value to find the maximum value

            bool hasTile = false; // Whether any Tile is found

            // Traverse the Tilemap to find the filled minimum and maximum coordinates
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                for (int x = bounds.xMin; x < bounds.xMax; x++)
                {
                    Vector3Int pos = new Vector3Int(x, y, 0);
                    if (tilemap.HasTile(pos))
                    {
                        hasTile = true;
                        minX = Mathf.Min(minX, x);
                        minY = Mathf.Min(minY, y);
                        maxX = Mathf.Max(maxX, x);
                        maxY = Mathf.Max(maxY, y);
                    }
                }
            }

            if (!hasTile) return new BoundsInt(); // If no Tile, return an empty range

            return new BoundsInt(new Vector3Int(minX, minY, 0), new Vector3Int(maxX - minX + 1, maxY - minY + 1, 1));
        }
    }
}