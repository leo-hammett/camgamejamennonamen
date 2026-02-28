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
        public void Initialize()
        {
            // TODO: Implement initialization
            IsReady = true;
        }
        
        /// <summary>
        /// Initialize with specific configuration
        /// </summary>
        public void Initialize(MapConfig config)
        {
            this.config = config;
            // TODO: Initialize tile strengths array
            tileStrengths = new float[config.mapWidth, config.mapHeight];
            IsReady = true;
        }
        
        /// <summary>
        /// Update system each frame
        /// </summary>
        public void UpdateSystem(float deltaTime)
        {
            // TODO: Implement update logic
        }
        
        /// <summary>
        /// Shutdown and cleanup
        /// </summary>
        public void Shutdown()
        {
            // TODO: Implement cleanup
            IsReady = false;
        }
        
        /// <summary>
        /// Reset to initial state
        /// </summary>
        public void Reset()
        {
            // TODO: Reset tile strengths
            if (tileStrengths != null)
            {
                System.Array.Clear(tileStrengths, 0, tileStrengths.Length);
            }
        }
        
        /// <summary>
        /// Get strength at tile position
        /// </summary>
        public float GetTileStrength(int tileX, int tileY)
        {
            if (!IsInBounds(tileX, tileY)) return 0f;
            return tileStrengths[tileX, tileY];
        }
        
        /// <summary>
        /// Get strength at world position
        /// </summary>
        public float GetStrengthAtWorldPos(Vector2 worldPos)
        {
            var tilePos = WorldToTilePos(worldPos);
            return GetTileStrength(tilePos.x, tilePos.y);
        }
        
        /// <summary>
        /// Add strength to a tile
        /// </summary>
        public void AddTileStrength(int tileX, int tileY, float amount)
        {
            if (!IsInBounds(tileX, tileY)) return;
            tileStrengths[tileX, tileY] = Mathf.Min(1f, tileStrengths[tileX, tileY] + amount);
        }
        
        /// <summary>
        /// Add strength in radius with falloff
        /// </summary>
        public void AddStrengthInRadius(Vector2 worldCenter, float radius, float strength, AnimationCurve falloffCurve)
        {
            // TODO: Implement radius-based strengthening
        }
        
        /// <summary>
        /// Convert world to tile position
        /// </summary>
        public Vector2Int WorldToTilePos(Vector2 worldPos)
        {
            // TODO: Implement proper conversion using tilemap
            return new Vector2Int(Mathf.FloorToInt(worldPos.x), Mathf.FloorToInt(worldPos.y));
        }
        
        /// <summary>
        /// Convert tile to world position
        /// </summary>
        public Vector2 TileToWorldPos(Vector2Int tilePos)
        {
            // TODO: Implement proper conversion using tilemap
            return new Vector2(tilePos.x + 0.5f, tilePos.y + 0.5f);
        }
        
        /// <summary>
        /// Check if position is in bounds
        /// </summary>
        public bool IsInBounds(int tileX, int tileY)
        {
            if (config == null || tileStrengths == null) return false;
            return tileX >= 0 && tileX < config.mapWidth && tileY >= 0 && tileY < config.mapHeight;
        }
        
        /// <summary>
        /// Check if position is a gravestone
        /// </summary>
        public bool IsGravestone(Vector2 worldPos)
        {
            return GetStrengthAtWorldPos(worldPos) >= 1.0f;
        }
        
        /// <summary>
        /// Get tiles in rectangular area
        /// </summary>
        public TileData[,] GetTilesInArea(Vector2Int minTile, Vector2Int maxTile)
        {
            // TODO: Implement area query
            return new TileData[0, 0];
        }
        
        /// <summary>
        /// Update tile visual representation
        /// </summary>
        public void UpdateTileVisuals()
        {
            // TODO: Update tilemap visuals based on strength values
        }
        
        /// <summary>
        /// Get Unity tilemap
        /// </summary>
        public Tilemap GetTilemap()
        {
            return tilemap;
        }
        
        /// <summary>
        /// Get map dimensions
        /// </summary>
        public Vector2Int GetMapDimensions()
        {
            if (config == null) return Vector2Int.zero;
            return new Vector2Int(config.mapWidth, config.mapHeight);
        }
        
        /// <summary>
        /// Clear all stone
        /// </summary>
        public void ClearAllStone()
        {
            Reset();
            UpdateTileVisuals();
        }
    }
}