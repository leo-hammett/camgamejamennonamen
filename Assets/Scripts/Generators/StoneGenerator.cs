using UnityEngine;
using InGraved.Config;

namespace InGraved.Generators
{
    /// <summary>
    /// Concrete implementation of a stone generator
    /// </summary>
    public class StoneGenerator : MonoBehaviour, IStoneGenerator
    {
        private static int nextId = 0;
        private GeneratorConfig config;
        
        public int GeneratorId { get; private set; }
        public Vector2 Position => transform.position;
        public Vector2 Velocity { get; private set; }
        public float CurrentRadius { get; private set; }
        public float AliveTime { get; private set; }
        public bool IsAlive { get; private set; }
        
        /// <summary>
        /// Initialize generator with position and target
        /// </summary>
        public void Initialize(Vector2 position, Transform targetPlayer)
        {
            GeneratorId = nextId++;
            transform.position = position;
            CurrentRadius = config?.initialRadius ?? 2f;
            AliveTime = 0f;
            IsAlive = true;
            // TODO: Set target player
        }
        
        /// <summary>
        /// Update generator each frame
        /// </summary>
        public void UpdateGenerator(float deltaTime, float currentStoneStrength)
        {
            if (!IsAlive) return;
            AliveTime += deltaTime;
            // TODO: Update movement and radius growth
        }
        
        /// <summary>
        /// Apply stone effect to map
        /// </summary>
        public void ApplyStoneStrengthening(Map.IMapSystem mapSystem)
        {
            if (!IsAlive || mapSystem == null) return;
            // TODO: Apply strengthening in radius
        }
        
        /// <summary>
        /// Kill this generator
        /// </summary>
        public void Kill(bool playDeathEffect)
        {
            IsAlive = false;
            // TODO: Play death effect if requested
            Destroy(gameObject, 0.5f);
        }
        
        /// <summary>
        /// Freeze movement temporarily
        /// </summary>
        public void Freeze(float duration)
        {
            // TODO: Implement freeze logic
        }
        
        /// <summary>
        /// Slow movement temporarily
        /// </summary>
        public void Slow(float slowFactor, float duration)
        {
            // TODO: Implement slow logic
        }
        
        /// <summary>
        /// Update chase target
        /// </summary>
        public void SetTarget(Transform target)
        {
            // TODO: Set new target
        }
        
        /// <summary>
        /// Get distance to position
        /// </summary>
        public float GetDistanceTo(Vector2 position)
        {
            return Vector2.Distance(Position, position);
        }
    }
}