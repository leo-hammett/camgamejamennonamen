
using UnityEngine;
using InGraved.Config;

namespace InGraved.Generators
{
    /// <summary>
    /// MIGRATION NOTE: Currently using StoneGrowerControl.cs for working enemies
    /// 
    /// When ready to adopt this architecture:
    /// 1. Move chase logic from StoneGrowerControl.Update() here
    /// 2. Move stone painting from StoneGrowerControl.PaintStone() to ApplyStoneStrengthening()
    /// 3. Use GeneratorConfig for speed/radius values instead of hardcoded
    /// 4. Integrate with GeneratorManager for proper spawning/pooling
    /// 
    /// Benefits of migrating:
    /// - Object pooling for better performance
    /// - Configurable difficulty curves via ScriptableObjects
    /// - Proper separation of movement vs stone spreading
    /// - Power-up system integration (freeze/slow)
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
        
        /* COMMENTED OUT - Using StoneGrowerControl.cs for now
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
            // TODO: Port from StoneGrowerControl:
            // - Set target player reference
            // - Initialize tilemap references
        }
        */
        
        /* COMMENTED OUT - Using StoneGrowerControl.cs for now
        /// <summary>
        /// Update generator each frame
        /// </summary>
        public void UpdateGenerator(float deltaTime, float currentStoneStrength)
        {
            if (!IsAlive) return;
            AliveTime += deltaTime;
            // TODO: Port from StoneGrowerControl.Update():
            // - Chase player logic
            // - Radius growth over time
            // - Speed scaling on stone
        }
        */
        
        /* COMMENTED OUT - Using StoneGrowerControl.cs for now  
        /// <summary>
        /// Apply stone effect to map
        /// </summary>
        public void ApplyStoneStrengthening(Map.IMapSystem mapSystem)
        {
            if (!IsAlive || mapSystem == null) return;
            // TODO: Port from StoneGrowerControl.PaintStone():
            // - Calculate tiles in radius
            // - Apply strength values instead of just painting
            // - Use MapSystem's tile strength tracking
        }
        */
        
        // Stub implementations to satisfy interface
        public void Initialize(Vector2 position, Transform targetPlayer) { }
        public void UpdateGenerator(float deltaTime, float currentStoneStrength) { }
        public void ApplyStoneStrengthening(Map.IMapSystem mapSystem) { }
        
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

