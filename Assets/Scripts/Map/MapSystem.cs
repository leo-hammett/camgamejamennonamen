#if false
using UnityEngine;
using UnityEngine.Tilemaps;
using InGraved.Core;
using InGraved.Config;

namespace InGraved.Map
{
    /// <summary>
    /// Concrete implementation of the map system
    /// </summary>
    public class MapSystem : MonoBehaviour, IMapSystem
    {
        [Header("References")]
        public Tilemap tilemap;
        public TileBase[] strengthBandTiles;
        
        private MapConfig config;
        private float[,] tileStrengths;
        
        public bool IsReady { get; private set; }
        
        /// <summary>
        /// Initialize the system
        /// </summary>
        public void Initialize();
        
        /// <summary>
        /// Initialize with specific configuration
        /// </summary>
        public void Initialize(MapConfig config);
        
        /// <summary>
        /// Update system each frame
        /// </summary>
        public void UpdateSystem(float deltaTime);
        
        /// <summary>
        /// Shutdown and cleanup
        /// </summary>
        public void Shutdown();
        
        /// <summary>
        /// Reset to initial state
        /// </summary>
        public void Reset();
        
        /// <summary>
        /// Get strength at tile position
        /// </summary>
        public float GetTileStrength(int tileX, int tileY);
        
        /// <summary>
        /// Get strength at world position
        /// </summary>
        public float GetStrengthAtWorldPos(Vector2 worldPos);
        
        /// <summary>
        /// Add strength to a tile
        /// </summary>
        public void AddTileStrength(int tileX, int tileY, float amount);
        
        /// <summary>
        /// Add strength in radius with falloff
        /// </summary>
        public void AddStrengthInRadius(Vector2 worldCenter, float radius, float strength, AnimationCurve falloffCurve);
        
        /// <summary>
        /// Convert world to tile position
        /// </summary>
        public Vector2Int WorldToTilePos(Vector2 worldPos);
        
        /// <summary>
        /// Convert tile to world position
        /// </summary>
        public Vector2 TileToWorldPos(Vector2Int tilePos);
        
        /// <summary>
        /// Check if position is in bounds
        /// </summary>
        public bool IsInBounds(int tileX, int tileY);
        
        /// <summary>
        /// Check if position is a gravestone
        /// </summary>
        public bool IsGravestone(Vector2 worldPos);
        
        /// <summary>
        /// Get tiles in rectangular area
        /// </summary>
        public TileData[,] GetTilesInArea(Vector2Int minTile, Vector2Int maxTile);
        
        /// <summary>
        /// Update tile visual representation
        /// </summary>
        public void UpdateTileVisuals();
        
        /// <summary>
        /// Get Unity tilemap
        /// </summary>
        public Tilemap GetTilemap();
        
        /// <summary>
        /// Get map dimensions
        /// </summary>
        public Vector2Int GetMapDimensions();
        
        /// <summary>
        /// Clear all stone
        /// </summary>
        public void ClearAllStone();
    }
}
#endif
