
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
        public void Initialize()
        {
            IsAlive = true;
            IsReady = true;
        }
        
        /// <summary>
        /// Initialize with configuration
        /// </summary>
        public void Initialize(PlayerConfig config)
        {
            this.config = config;
            IsAlive = true;
            IsReady = true;
        }
        
        /// <summary>
        /// Update system
        /// </summary>
        public void UpdateSystem(float deltaTime)
        {
            // TODO: Update player system
        }
        
        /// <summary>
        /// Shutdown system
        /// </summary>
        public void Shutdown()
        {
            IsReady = false;
        }
        
        /// <summary>
        /// Reset system
        /// </summary>
        public void Reset()
        {
            IsAlive = true;
            Velocity = Vector2.zero;
        }
        
        /// <summary>
        /// Handle touch input
        /// </summary>
        public void HandleInput(Vector2 touchPosition, TouchPhase touchPhase)
        {
            // TODO: Handle touch/finger input
        }
        
        /// <summary>
        /// Update movement based on input
        /// </summary>
        public void UpdateMovement(float deltaTime)
        {
            if (!IsAlive) return;
            // TODO: Update movement
        }
        
        /// <summary>
        /// Check for gravestone collision
        /// </summary>
        public bool CheckGravestoneCollision(Map.IMapSystem mapSystem)
        {
            if (mapSystem == null) return false;
            return mapSystem.IsGravestone(Position);
        }
        
        /// <summary>
        /// Kill the player
        /// </summary>
        public void Kill()
        {
            IsAlive = false;
            // TODO: Play death effect
        }
        
        /// <summary>
        /// Respawn at position
        /// </summary>
        public void Respawn(Vector2 position)
        {
            transform.position = position;
            IsAlive = true;
            Velocity = Vector2.zero;
        }
        
        /// <summary>
        /// Get player bounds
        /// </summary>
        public Bounds GetBounds()
        {
            var size = config?.collisionBoxSize ?? Vector2.one;
            return new Bounds(Position, size);
        }
        
        /// <summary>
        /// Enable/disable movement
        /// </summary>
        public void SetMovementEnabled(bool enabled)
        {
            // TODO: Implement movement enable/disable
        }
        
        /// <summary>
        /// Apply speed powerup
        /// </summary>
        public void ApplySpeedBoost(float multiplier, float duration)
        {
            // TODO: Implement speed boost
        }
        
        /// <summary>
        /// Apply invincibility powerup
        /// </summary>
        public void ApplyInvincibility(float duration)
        {
            // TODO: Implement invincibility
        }
    }
}

