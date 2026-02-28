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
        public void Initialize(Vector2 position, Transform targetPlayer);
        
        /// <summary>
        /// Update generator each frame
        /// </summary>
        public void UpdateGenerator(float deltaTime, float currentStoneStrength);
        
        /// <summary>
        /// Apply stone effect to map
        /// </summary>
        public void ApplyStoneStrengthening(Map.IMapSystem mapSystem);
        
        /// <summary>
        /// Kill this generator
        /// </summary>
        public void Kill(bool playDeathEffect);
        
        /// <summary>
        /// Freeze movement temporarily
        /// </summary>
        public void Freeze(float duration);
        
        /// <summary>
        /// Slow movement temporarily
        /// </summary>
        public void Slow(float slowFactor, float duration);
        
        /// <summary>
        /// Update chase target
        /// </summary>
        public void SetTarget(Transform target);
        
        /// <summary>
        /// Get distance to position
        /// </summary>
        public float GetDistanceTo(Vector2 position);
    }
}