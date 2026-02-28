using UnityEngine;
using UnityEngine.Tilemaps;
using InGraved.Core;
using InGraved.Config;

namespace InGraved.Map
{
    /// <summary>
    /// Interface for the map system that manages tiles and stone strength
    /// </summary>
    public interface IMapSystem : IGameSystem
    {
        /// <summary>
        /// Initialize map with configuration
        /// </summary>
        /// <param name="config">Map configuration</param>
        void Initialize(MapConfig config);
        
        /// <summary>
        /// Get stone strength at a specific tile position
        /// </summary>
        /// <param name="tileX">Tile X coordinate</param>
        /// <param name="tileY">Tile Y coordinate</param>
        /// <returns>Stone strength (0.0 to 1.0)</returns>
        float GetTileStrength(int tileX, int tileY);
        
        /// <summary>
        /// Get stone strength at a world position
        /// </summary>
        /// <param name="worldPos">World position</param>
        /// <returns>Stone strength (0.0 to 1.0)</returns>
        float GetStrengthAtWorldPos(Vector2 worldPos);
        
        /// <summary>
        /// Add stone strength to a tile (clamped at 1.0)
        /// </summary>
        /// <param name="tileX">Tile X coordinate</param>
        /// <param name="tileY">Tile Y coordinate</param>
        /// <param name="amount">Amount to add</param>
        void AddTileStrength(int tileX, int tileY, float amount);
        
        /// <summary>
        /// Add stone strength in a circular area
        /// </summary>
        /// <param name="worldCenter">Center position in world space</param>
        /// <param name="radius">Radius of effect</param>
        /// <param name="strength">Base strength to add</param>
        /// <param name="falloffCurve">Falloff from center to edge</param>
        void AddStrengthInRadius(Vector2 worldCenter, float radius, float strength, AnimationCurve falloffCurve);
        
        /// <summary>
        /// Convert world position to tile coordinates
        /// </summary>
        /// <param name="worldPos">World position</param>
        /// <returns>Tile coordinates</returns>
        Vector2Int WorldToTilePos(Vector2 worldPos);
        
        /// <summary>
        /// Convert tile coordinates to world position (center of tile)
        /// </summary>
        /// <param name="tilePos">Tile coordinates</param>
        /// <returns>World position</returns>
        Vector2 TileToWorldPos(Vector2Int tilePos);
        
        /// <summary>
        /// Check if a tile position is within map bounds
        /// </summary>
        /// <param name="tileX">Tile X coordinate</param>
        /// <param name="tileY">Tile Y coordinate</param>
        /// <returns>True if within bounds</returns>
        bool IsInBounds(int tileX, int tileY);
        
        /// <summary>
        /// Check if world position has a gravestone (strength = 1.0)
        /// </summary>
        /// <param name="worldPos">World position</param>
        /// <returns>True if gravestone</returns>
        bool IsGravestone(Vector2 worldPos);
        
        /// <summary>
        /// Get all tiles in a rectangular area
        /// </summary>
        /// <param name="minTile">Min tile coordinates</param>
        /// <param name="maxTile">Max tile coordinates</param>
        /// <returns>Array of tile data</returns>
        TileData[,] GetTilesInArea(Vector2Int minTile, Vector2Int maxTile);
        
        /// <summary>
        /// Update visual representation of tiles
        /// </summary>
        void UpdateTileVisuals();
        
        /// <summary>
        /// Get the Unity Tilemap component
        /// </summary>
        Tilemap GetTilemap();
        
        /// <summary>
        /// Get map dimensions
        /// </summary>
        Vector2Int GetMapDimensions();
        
        /// <summary>
        /// Clear all stone (reset to initial state)
        /// </summary>
        void ClearAllStone();
    }
    
    /// <summary>
    /// Data structure for a single tile
    /// </summary>
    [System.Serializable]
    public struct TileData
    {
        public int x;
        public int y;
        public float strength;
        public int visualBand;
    }
}