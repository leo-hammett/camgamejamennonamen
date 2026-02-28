
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
        public void Initialize(Transform playerTransform)
        {
            target = playerTransform;
        }
        
        /// <summary>
        /// Update camera position smoothly
        /// </summary>
        public void UpdateCamera()
        {
            if (target == null || mapConfig == null) return;
            
            Vector3 desiredPos = target.position + mapConfig.cameraOffset;
            transform.position = Vector3.Lerp(transform.position, desiredPos, 
                Time.deltaTime * mapConfig.cameraFollowSpeed);
        }
        
        /// <summary>
        /// Set camera bounds based on map size
        /// </summary>
        /// <param name="mapBounds">Map boundaries</param>
        public void SetBounds(Bounds mapBounds)
        {
            // TODO: Implement camera bounds clamping
        }
        
        /// <summary>
        /// Shake camera for impact effect
        /// </summary>
        /// <param name="intensity">Shake intensity</param>
        /// <param name="duration">Shake duration</param>
        public void ShakeCamera(float intensity, float duration)
        {
            // TODO: Implement camera shake
        }
        
        /// <summary>
        /// Zoom camera
        /// </summary>
        /// <param name="zoomLevel">Target zoom level</param>
        /// <param name="duration">Zoom duration</param>
        public void SetZoom(float zoomLevel, float duration)
        {
            // TODO: Implement camera zoom
        }
        
        /// <summary>
        /// Get current camera bounds
        /// </summary>
        /// <returns>Camera view bounds</returns>
        public Bounds GetViewBounds()
        {
            Camera cam = Camera.main;
            if (cam == null) return new Bounds();
            
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;
            
            return new Bounds(transform.position, new Vector3(width, height, 0));
        }
        
        /// <summary>
        /// Convert screen position to world position
        /// </summary>
        /// <param name="screenPos">Screen position</param>
        /// <returns>World position</returns>
        public Vector2 ScreenToWorldPoint(Vector2 screenPos)
        {
            Camera cam = Camera.main;
            if (cam == null) return Vector2.zero;
            
            Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
            return new Vector2(worldPos.x, worldPos.y);
        }
    }
}

