using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map
{
    public class Room : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Tilemap _tilemap;

        [Button]
        public BoundsInt GetBoundX()
        {
            BoundsInt filledBounds = GetFilledTileBounds();
            // Debug.Log($"{name}: {filledBounds.size.x}");
            // Debug.Log($"Filled Bounds - xMin: {filledBounds.xMin}, xMax: {filledBounds.xMax}");
            
            return filledBounds;
        }
        
        BoundsInt GetFilledTileBounds()
        {
            if (_tilemap == null) return new BoundsInt();

            BoundsInt bounds = _tilemap.cellBounds;
            int minX = bounds.xMax, minY = bounds.yMax; // Set to maximum value to find the minimum value
            int maxX = bounds.xMin, maxY = bounds.yMin; // Set to minimum value to find the maximum value

            bool hasTile = false; // Whether any Tile is found

            // Traverse the Tilemap to find the filled minimum and maximum coordinates
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                for (int x = bounds.xMin; x < bounds.xMax; x++)
                {
                    Vector3Int pos = new Vector3Int(x, y, 0);
                    if (_tilemap.HasTile(pos))
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