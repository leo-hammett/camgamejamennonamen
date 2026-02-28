#if false
using UnityEngine;
using System.Collections.Generic;
using InGraved.Core;
using InGraved.Config;

namespace InGraved.Generators
{
    /// <summary>
    /// Interface for managing all stone generators
    /// </summary>
    public interface IGeneratorManager : IGameSystem
    {
        /// <summary>
        /// Initialize with configuration
        /// </summary>
        /// <param name="config">Generator configuration</param>
        void Initialize(GeneratorConfig config);
        
        /// <summary>
        /// Spawn a new generator
        /// </summary>
        /// <param name="position">World position to spawn at</param>
        /// <returns>The spawned generator, or null if spawn failed</returns>
        IStoneGenerator SpawnGenerator(Vector2 position);
        
        /// <summary>
        /// Spawn a generator at a random valid position
        /// </summary>
        /// <returns>The spawned generator, or null if spawn failed</returns>
        IStoneGenerator SpawnRandomGenerator();
        
        /// <summary>
        /// Kill a specific generator
        /// </summary>
        /// <param name="generator">Generator to kill</param>
        /// <param name="wasEncircled">True if killed by encirclement</param>
        void KillGenerator(IStoneGenerator generator, bool wasEncircled);
        
        /// <summary>
        /// Kill all generators in a list
        /// </summary>
        /// <param name="generators">Generators to kill</param>
        /// <param name="wasEncircled">True if killed by encirclement</param>
        void KillGenerators(List<IStoneGenerator> generators, bool wasEncircled);
        
        /// <summary>
        /// Get all active generators
        /// </summary>
        /// <returns>List of active generators</returns>
        List<IStoneGenerator> GetActiveGenerators();
        
        /// <summary>
        /// Get generators within a radius of a position
        /// </summary>
        /// <param name="center">Center position</param>
        /// <param name="radius">Search radius</param>
        /// <returns>List of generators in radius</returns>
        List<IStoneGenerator> GetGeneratorsInRadius(Vector2 center, float radius);
        
        /// <summary>
        /// Check if a generator is encircled by a trail
        /// </summary>
        /// <param name="generator">Generator to check</param>
        /// <param name="trail">Trail points</param>
        /// <returns>True if encircled</returns>
        bool IsGeneratorEncircled(IStoneGenerator generator, List<Vector2> trail);
        
        /// <summary>
        /// Update spawn rate based on difficulty
        /// </summary>
        /// <param name="multiplier">Spawn rate multiplier</param>
        void SetSpawnRateMultiplier(float multiplier);
        
        /// <summary>
        /// Get current number of active generators
        /// </summary>
        int GetActiveGeneratorCount();
        
        /// <summary>
        /// Get total generators killed
        /// </summary>
        int GetTotalGeneratorsKilled();
        
        /// <summary>
        /// Clear all generators
        /// </summary>
        void ClearAllGenerators();
        
        /// <summary>
        /// HOOK: Future powerup - freeze all generators
        /// </summary>
        /// <param name="duration">Freeze duration</param>
        void FreezeAllGenerators(float duration);
        
        /// <summary>
        /// HOOK: Future powerup - slow all generators
        /// </summary>
        /// <param name="slowFactor">Speed multiplier (0.5 = half speed)</param>
        /// <param name="duration">Slow duration</param>
        void SlowAllGenerators(float slowFactor, float duration);
    }
}
#endif
