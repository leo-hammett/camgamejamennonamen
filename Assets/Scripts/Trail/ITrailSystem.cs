
using UnityEngine;
using System.Collections.Generic;
using InGraved.Core;

namespace InGraved.Trail
{
    /// <summary>
    /// Interface for the trail system that follows the player
    /// </summary>
    public interface ITrailSystem : IGameSystem
    {
        /// <summary>
        /// Current trail points
        /// </summary>
        List<Vector2> TrailPoints { get; }
        
        /// <summary>
        /// Trail length
        /// </summary>
        int TrailLength { get; }
        
        /// <summary>
        /// Add a new point to the trail
        /// </summary>
        /// <param name="position">Position to add</param>
        void AddTrailPoint(Vector2 position);
        
        /// <summary>
        /// Update trail (remove old points, fade, etc)
        /// </summary>
        /// <param name="deltaTime">Frame delta time</param>
        void UpdateTrail(float deltaTime);
        
        /// <summary>
        /// Clear the entire trail
        /// </summary>
        void ClearTrail();
        
        /// <summary>
        /// Check if a position is inside the trail loop
        /// </summary>
        /// <param name="position">Position to check</param>
        /// <returns>True if encircled by trail</returns>
        bool IsPositionEncircled(Vector2 position);
        
        /// <summary>
        /// Get all positions encircled by the trail
        /// </summary>
        /// <returns>List of encircled positions</returns>
        List<Vector2> GetEncircledPositions();
        
        /// <summary>
        /// Check if trail forms a closed loop
        /// </summary>
        /// <returns>True if trail is closed</returns>
        bool IsTrailClosed();
        
        /// <summary>
        /// Get the trail renderer component
        /// </summary>
        LineRenderer GetLineRenderer();
        
        /// <summary>
        /// Update visual representation of trail
        /// </summary>
        void UpdateTrailVisual();
        
        /// <summary>
        /// Set trail visibility
        /// </summary>
        /// <param name="visible">Visibility state</param>
        void SetTrailVisible(bool visible);
        
        /// <summary>
        /// HOOK: Apply trail enhancement powerup
        /// </summary>
        /// <param name="lengthMultiplier">Trail length multiplier</param>
        /// <param name="duration">Enhancement duration</param>
        void ApplyTrailEnhancement(float lengthMultiplier, float duration);
    }
}

