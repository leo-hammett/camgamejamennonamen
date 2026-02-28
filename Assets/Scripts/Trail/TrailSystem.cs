
using UnityEngine;
using System.Collections.Generic;
using InGraved.Config;

namespace InGraved.Trail
{
    /// <summary>
    /// Concrete implementation of the trail system
    /// </summary>
    public class TrailSystem : MonoBehaviour, ITrailSystem
    {
        [Header("References")]
        public LineRenderer lineRenderer;
        
        private PlayerConfig config;
        private List<Vector2> trailPoints = new List<Vector2>();
        
        public List<Vector2> TrailPoints => trailPoints;
        public int TrailLength => trailPoints.Count;
        public bool IsReady { get; private set; }
        
        /// <summary>
        /// Initialize system
        /// </summary>
        public void Initialize()
        {
            IsReady = true;
            trailPoints = new List<Vector2>();
        }
        
        /// <summary>
        /// Update system
        /// </summary>
        public void UpdateSystem(float deltaTime)
        {
            UpdateTrail(deltaTime);
        }
        
        /// <summary>
        /// Shutdown system
        /// </summary>
        public void Shutdown()
        {
            IsReady = false;
            ClearTrail();
        }
        
        /// <summary>
        /// Reset system
        /// </summary>
        public void Reset()
        {
            ClearTrail();
        }
        
        /// <summary>
        /// Add point to trail
        /// </summary>
        public void AddTrailPoint(Vector2 position)
        {
            if (config == null) return;
            
            // Check minimum distance from last point
            if (trailPoints.Count > 0)
            {
                float dist = Vector2.Distance(position, trailPoints[trailPoints.Count - 1]);
                if (dist < config.minTrailPointDistance) return;
            }
            
            trailPoints.Add(position);
            
            // Limit trail length
            while (trailPoints.Count > config.maxTrailPoints)
            {
                trailPoints.RemoveAt(0);
            }
            
            UpdateTrailVisual();
        }
        
        /// <summary>
        /// Update trail logic
        /// </summary>
        public void UpdateTrail(float deltaTime)
        {
            // TODO: Implement trail fade/update logic
        }
        
        /// <summary>
        /// Clear all trail points
        /// </summary>
        public void ClearTrail()
        {
            trailPoints.Clear();
            UpdateTrailVisual();
        }
        
        /// <summary>
        /// Check if position is encircled
        /// </summary>
        public bool IsPositionEncircled(Vector2 position)
        {
            if (!IsTrailClosed()) return false;
            return EncirclementDetector.IsPointInPolygon(position, trailPoints);
        }
        
        /// <summary>
        /// Get all encircled positions
        /// </summary>
        public List<Vector2> GetEncircledPositions()
        {
            // TODO: Implement encircled position detection
            return new List<Vector2>();
        }
        
        /// <summary>
        /// Check if trail forms closed loop
        /// </summary>
        public bool IsTrailClosed()
        {
            return EncirclementDetector.IsTrailClosed(trailPoints);
        }
        
        /// <summary>
        /// Get line renderer
        /// </summary>
        public LineRenderer GetLineRenderer()
        {
            return lineRenderer;
        }
        
        /// <summary>
        /// Update trail visual
        /// </summary>
        public void UpdateTrailVisual()
        {
            if (lineRenderer == null) return;
            
            lineRenderer.positionCount = trailPoints.Count;
            for (int i = 0; i < trailPoints.Count; i++)
            {
                lineRenderer.SetPosition(i, trailPoints[i]);
            }
        }
        
        /// <summary>
        /// Set trail visibility
        /// </summary>
        public void SetTrailVisible(bool visible)
        {
            if (lineRenderer != null)
                lineRenderer.enabled = visible;
        }
        
        /// <summary>
        /// Apply trail powerup
        /// </summary>
        public void ApplyTrailEnhancement(float lengthMultiplier, float duration)
        {
            // TODO: Implement trail enhancement
        }
    }
}

