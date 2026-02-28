using UnityEngine;

namespace InGraved.Generators
{
    /// <summary>
    /// Interface for individual stone generator behavior
    /// </summary>
    public interface IStoneGenerator
    {
        /// <summary>
        /// Unique identifier for this generator
        /// </summary>
        int GeneratorId { get; }
        
        /// <summary>
        /// Current world position
        /// </summary>
        Vector2 Position { get; }
        
        /// <summary>
        /// Current movement velocity
        /// </summary>
        Vector2 Velocity { get; }
        
        /// <summary>
        /// Current effect radius
        /// </summary>
        float CurrentRadius { get; }
        
        /// <summary>
        /// Time since spawn
        /// </summary>
        float AliveTime { get; }
        
        /// <summary>
        /// Is this generator active and alive
        /// </summary>
        bool IsAlive { get; }
        
        /// <summary>
        /// Initialize the generator
        /// </summary>
        /// <param name="position">Starting position</param>
        /// <param name="targetPlayer">Player to chase</param>
        void Initialize(Vector2 position, Transform targetPlayer);
        
        /// <summary>
        /// Update generator logic
        /// </summary>
        /// <param name="deltaTime">Frame delta time</param>
        /// <param name="currentStoneStrength">Stone strength at current position</param>
        void UpdateGenerator(float deltaTime, float currentStoneStrength);
        
        /// <summary>
        /// Apply stone strengthening to map
        /// </summary>
        /// <param name="mapSystem">Map system to modify</param>
        void ApplyStoneStrengthening(Map.IMapSystem mapSystem);
        
        /// <summary>
        /// Kill this generator
        /// </summary>
        /// <param name="playDeathEffect">Whether to play death effect</param>
        void Kill(bool playDeathEffect);
        
        /// <summary>
        /// Freeze generator movement
        /// </summary>
        /// <param name="duration">Freeze duration</param>
        void Freeze(float duration);
        
        /// <summary>
        /// Slow generator movement
        /// </summary>
        /// <param name="slowFactor">Speed multiplier</param>
        /// <param name="duration">Slow duration</param>
        void Slow(float slowFactor, float duration);
        
        /// <summary>
        /// Set chase target
        /// </summary>
        /// <param name="target">New target transform</param>
        void SetTarget(Transform target);
        
        /// <summary>
        /// Get distance to a position
        /// </summary>
        /// <param name="position">Position to check</param>
        /// <returns>Distance</returns>
        float GetDistanceTo(Vector2 position);
    }
}