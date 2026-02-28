#if false
using UnityEngine;

namespace InGraved.Config
{
    [CreateAssetMenu(fileName = "MapConfig", menuName = "InGraved/Map Configuration", order = 1)]
    public class MapConfig : ScriptableObject
    {
        [Header("Map Dimensions")]
        [Tooltip("Width of the map in tiles")]
        public int mapWidth = 100;
        
        [Tooltip("Height of the map in tiles")]
        public int mapHeight = 100;
        
        [Header("Tile Properties")]
        [Tooltip("Size of each tile in Unity units")]
        public float tileSize = 1.0f;
        
        [Tooltip("Initial stone strength for all tiles")]
        public float initialStrength = 0.0f;
        
        [Header("Strength Visual Bands")]
        [Tooltip("Number of visual bands for stone strength (0.0 to 1.0 split into bands)")]
        public int strengthBandCount = 10;
        
        [Tooltip("Colors for each strength band (index 0 = weakest, last = strongest)")]
        public Color[] bandColors;
        
        [Header("Camera")]
        [Tooltip("How smoothly the camera follows the player")]
        public float cameraFollowSpeed = 5.0f;
        
        [Tooltip("Camera offset from player position")]
        public Vector3 cameraOffset = new Vector3(0, 0, -10);
    }
}
#endif
