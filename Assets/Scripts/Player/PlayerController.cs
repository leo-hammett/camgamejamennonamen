using UnityEngine;
using InGraved.Config;
using InGraved.Trail;

namespace InGraved.Player
{
    /// <summary>
    /// Concrete implementation of player controller
    /// </summary>
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        [Header("References")]
        public TrailSystem trailSystem;
        
        private PlayerConfig config;
        
        public Vector2 Position => transform.position;
        public Vector2 Velocity { get; private set; }
        public bool IsAlive { get; private set; }
        public bool IsReady { get; private set; }
        
        /// <summary>
        /// Initialize the system
        /// </summary>
        public void Initialize();
        
        /// <summary>
        /// Initialize with configuration
        /// </summary>
        public void Initialize(PlayerConfig config);
        
        /// <summary>
        /// Update system
        /// </summary>
        public void UpdateSystem(float deltaTime);
        
        /// <summary>
        /// Shutdown system
        /// </summary>
        public void Shutdown();
        
        /// <summary>
        /// Reset system
        /// </summary>
        public void Reset();
        
        /// <summary>
        /// Handle touch input
        /// </summary>
        public void HandleInput(Vector2 touchPosition, TouchPhase touchPhase);
        
        /// <summary>
        /// Update movement based on input
        /// </summary>
        public void UpdateMovement(float deltaTime);
        
        /// <summary>
        /// Check for gravestone collision
        /// </summary>
        public bool CheckGravestoneCollision(Map.IMapSystem mapSystem);
        
        /// <summary>
        /// Kill the player
        /// </summary>
        public void Kill();
        
        /// <summary>
        /// Respawn at position
        /// </summary>
        public void Respawn(Vector2 position);
        
        /// <summary>
        /// Get player bounds
        /// </summary>
        public Bounds GetBounds();
        
        /// <summary>
        /// Enable/disable movement
        /// </summary>
        public void SetMovementEnabled(bool enabled);
        
        /// <summary>
        /// Apply speed powerup
        /// </summary>
        public void ApplySpeedBoost(float multiplier, float duration);
        
        /// <summary>
        /// Apply invincibility powerup
        /// </summary>
        public void ApplyInvincibility(float duration);
    }
}