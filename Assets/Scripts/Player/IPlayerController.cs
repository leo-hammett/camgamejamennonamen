#if false
using UnityEngine;
using System.Collections.Generic;
using InGraved.Core;
using InGraved.Config;

namespace InGraved.Player
{
    /// <summary>
    /// Interface for player controller system
    /// </summary>
    public interface IPlayerController : IGameSystem
    {
        /// <summary>
        /// Current player position
        /// </summary>
        Vector2 Position { get; }
        
        /// <summary>
        /// Current player velocity
        /// </summary>
        Vector2 Velocity { get; }
        
        /// <summary>
        /// Is player alive
        /// </summary>
        bool IsAlive { get; }
        
        /// <summary>
        /// Initialize with configuration
        /// </summary>
        /// <param name="config">Player configuration</param>
        void Initialize(PlayerConfig config);
        
        /// <summary>
        /// Handle touch/finger input
        /// </summary>
        /// <param name="touchPosition">Screen touch position</param>
        /// <param name="touchPhase">Touch phase (began, moved, ended)</param>
        void HandleInput(Vector2 touchPosition, TouchPhase touchPhase);
        
        /// <summary>
        /// Update player movement
        /// </summary>
        /// <param name="deltaTime">Frame delta time</param>
        void UpdateMovement(float deltaTime);
        
        /// <summary>
        /// Check collision with gravestones
        /// </summary>
        /// <param name="mapSystem">Map system to check against</param>
        /// <returns>True if hitting gravestone</returns>
        bool CheckGravestoneCollision(Map.IMapSystem mapSystem);
        
        /// <summary>
        /// Kill the player
        /// </summary>
        void Kill();
        
        /// <summary>
        /// Respawn player at position
        /// </summary>
        /// <param name="position">Spawn position</param>
        void Respawn(Vector2 position);
        
        /// <summary>
        /// Get player's bounding box
        /// </summary>
        /// <returns>Bounding box</returns>
        Bounds GetBounds();
        
        /// <summary>
        /// Set movement enabled/disabled
        /// </summary>
        /// <param name="enabled">Enable state</param>
        void SetMovementEnabled(bool enabled);
        
        /// <summary>
        /// HOOK: Apply speed boost powerup
        /// </summary>
        /// <param name="multiplier">Speed multiplier</param>
        /// <param name="duration">Boost duration</param>
        void ApplySpeedBoost(float multiplier, float duration);
        
        /// <summary>
        /// HOOK: Apply invincibility powerup
        /// </summary>
        /// <param name="duration">Invincibility duration</param>
        void ApplyInvincibility(float duration);
    }
}
#endif
