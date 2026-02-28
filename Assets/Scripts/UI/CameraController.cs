using UnityEngine;
using InGraved.Config;

namespace InGraved.UI
{
    /// <summary>
    /// Camera controller that follows the player
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [Header("Configuration")]
        public MapConfig mapConfig;
        
        [Header("References")]
        public Transform target;
        
        /// <summary>
        /// Initialize camera with target
        /// </summary>
        /// <param name="playerTransform">Player to follow</param>
        public void Initialize(Transform playerTransform);
        
        /// <summary>
        /// Update camera position smoothly
        /// </summary>
        public void UpdateCamera();
        
        /// <summary>
        /// Set camera bounds based on map size
        /// </summary>
        /// <param name="mapBounds">Map boundaries</param>
        public void SetBounds(Bounds mapBounds);
        
        /// <summary>
        /// Shake camera for impact effect
        /// </summary>
        /// <param name="intensity">Shake intensity</param>
        /// <param name="duration">Shake duration</param>
        public void ShakeCamera(float intensity, float duration);
        
        /// <summary>
        /// Zoom camera
        /// </summary>
        /// <param name="zoomLevel">Target zoom level</param>
        /// <param name="duration">Zoom duration</param>
        public void SetZoom(float zoomLevel, float duration);
        
        /// <summary>
        /// Get current camera bounds
        /// </summary>
        /// <returns>Camera view bounds</returns>
        public Bounds GetViewBounds();
        
        /// <summary>
        /// Convert screen position to world position
        /// </summary>
        /// <param name="screenPos">Screen position</param>
        /// <returns>World position</returns>
        public Vector2 ScreenToWorldPoint(Vector2 screenPos);
    }
}