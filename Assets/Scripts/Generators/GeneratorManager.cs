// Removed #if false to enable compilation
using UnityEngine;
using System.Collections.Generic;
using InGraved.Core;
using InGraved.Config;

namespace InGraved.Generators
{
    /// <summary>
    /// HYBRID MANAGER: Works with both StoneGrowerControl (current) and StoneGenerator (future)
    /// 
    /// Current Setup (for testing):
    /// - Create StoneGrowerControl prefab with sprites/tilemap refs
    /// - Assign prefab to generatorPrefabOld field
    /// - Manager will spawn them at configured rate
    /// 
    /// Future Migration:
    /// - Switch to StoneGenerator prefab instead
    /// - Uncomment the new architecture code
    /// - Remove StoneGrowerControl references
    /// </summary>
    public class GeneratorManager : MonoBehaviour, IGeneratorManager
    {
        [Header("Configuration")]
        public GeneratorConfig config;
        
        [Header("Temporary - Using Old Scripts")]
        public GameObject generatorPrefabOld; // StoneGrowerControl prefab
        public Tilemap stoneTilemap; // For StoneGrowerControl
        public TileBase stoneTile; // For StoneGrowerControl
        
        private List<GameObject> activeGeneratorsOld = new List<GameObject>(); // For StoneGrowerControl
        private List<IStoneGenerator> activeGenerators = new List<IStoneGenerator>(); // For future
        private float spawnTimer = 0f;
        private float spawnRateMultiplier = 1f;
        private int totalKilled = 0;
        
        public bool IsReady { get; private set; }
        
        /// <summary>
        /// Initialize the system
        /// </summary>
        public void Initialize()
        {
            activeGenerators = new List<IStoneGenerator>();
            IsReady = true;
        }
        
        /// <summary>
        /// Initialize with configuration
        /// </summary>
        public void Initialize(GeneratorConfig config)
        {
            this.config = config;
            activeGenerators = new List<IStoneGenerator>();
            IsReady = true;
        }
        
        /// <summary>
        /// Update system each frame
        /// </summary>
        public void UpdateSystem(float deltaTime)
        {
            if (!IsReady || config == null) return;
            
            // Update spawn timer
            spawnTimer -= deltaTime;
            if (spawnTimer <= 0f && activeGeneratorsOld.Count < config.maxAliveGenerators)
            {
                SpawnRandomGeneratorOld(); // Using old system for now
                spawnTimer = 1f / (config.baseSpawnRate * spawnRateMultiplier);
            }
            
            // Clean up destroyed generators
            activeGeneratorsOld.RemoveAll(g => g == null);
            
            // Update all generators
            foreach (var generator in activeGenerators)
            {
                if (generator.IsAlive)
                {
                    generator.UpdateGenerator(deltaTime, 0f); // TODO: Get actual stone strength
                }
            }
        }
        
        /// <summary>
        /// Shutdown and cleanup
        /// </summary>
        public void Shutdown()
        {
            ClearAllGenerators();
            IsReady = false;
        }
        
        /// <summary>
        /// Reset to initial state
        /// </summary>
        public void Reset()
        {
            ClearAllGenerators();
            spawnTimer = 0f;
            spawnRateMultiplier = 1f;
            totalKilled = 0;
        }
        
        /// <summary>
        /// Spawn a new generator
        /// </summary>
        public IStoneGenerator SpawnGenerator(Vector2 position)
        {
            if (!IsReady || config == null || config.generatorPrefab == null) return null;
            if (activeGenerators.Count >= config.maxAliveGenerators) return null;
            
            var generatorObj = Instantiate(config.generatorPrefab, position, Quaternion.identity);
            var generator = generatorObj.GetComponent<IStoneGenerator>();
            
            if (generator == null)
            {
                generator = generatorObj.AddComponent<StoneGenerator>();
            }
            
            var player = GameObject.FindWithTag("Player");
            generator.Initialize(position, player?.transform);
            
            activeGenerators.Add(generator);
            return generator;
        }
        
        /// <summary>
        /// Spawn generator using OLD StoneGrowerControl (temporary)
        /// </summary>
        private void SpawnRandomGeneratorOld()
        {
            if (generatorPrefabOld == null) 
            {
                Debug.LogWarning("No old generator prefab assigned! Set generatorPrefabOld in GeneratorManager.");
                return;
            }
            
            Vector2 spawnPos = GetRandomEdgePosition();
            GameObject newGen = Instantiate(generatorPrefabOld, spawnPos, Quaternion.identity);
            
            // Set up StoneGrowerControl references if needed
            var grower = newGen.GetComponent<StoneGrowerControl>();
            if (grower != null)
            {
                // Find PlayerMovement in scene
                var player = FindFirstObjectByType<PlayerMovement>();
                if (player != null)
                {
                    // Set via serialized field (needs to be public in StoneGrowerControl)
                    var field = typeof(StoneGrowerControl).GetField("movement", 
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    field?.SetValue(grower, player);
                }
                
                // Set tilemap references
                if (stoneTilemap != null)
                {
                    var tilemapField = typeof(StoneGrowerControl).GetField("stoneTilemap",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    tilemapField?.SetValue(grower, stoneTilemap);
                    
                    var tileField = typeof(StoneGrowerControl).GetField("stoneTile",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    tileField?.SetValue(grower, stoneTile);
                }
            }
            
            activeGeneratorsOld.Add(newGen);
        }
        
        /// <summary>
        /// Spawn a generator at random position (FUTURE)
        /// </summary>
        public IStoneGenerator SpawnRandomGenerator()
        {
            if (config == null) return null;
            
            // TODO: Migrate to new architecture when ready
            // Vector2 randomPos = Random.insideUnitCircle * config.maxSpawnDistance;
            // return SpawnGenerator(randomPos);
            return null;
        }
        
        /// <summary>
        /// Kill a specific generator
        /// </summary>
        public void KillGenerator(IStoneGenerator generator, bool wasEncircled)
        {
            if (generator == null) return;
            
            generator.Kill(wasEncircled);
            activeGenerators.Remove(generator);
            totalKilled++;
        }
        
        /// <summary>
        /// Kill multiple generators
        /// </summary>
        public void KillGenerators(List<IStoneGenerator> generators, bool wasEncircled)
        {
            foreach (var gen in generators)
            {
                KillGenerator(gen, wasEncircled);
            }
        }
        
        /// <summary>
        /// Get all active generators
        /// </summary>
        public List<IStoneGenerator> GetActiveGenerators()
        {
            return new List<IStoneGenerator>(activeGenerators);
        }
        
        /// <summary>
        /// Get generators in radius
        /// </summary>
        public List<IStoneGenerator> GetGeneratorsInRadius(Vector2 center, float radius)
        {
            var inRadius = new List<IStoneGenerator>();
            foreach (var gen in activeGenerators)
            {
                if (gen.IsAlive && gen.GetDistanceTo(center) <= radius)
                {
                    inRadius.Add(gen);
                }
            }
            return inRadius;
        }
        
        /// <summary>
        /// Check if generator is encircled
        /// </summary>
        public bool IsGeneratorEncircled(IStoneGenerator generator, List<Vector2> trail)
        {
            return Trail.EncirclementDetector.IsPointInPolygon(generator.Position, trail);
        }
        
        /// <summary>
        /// Set spawn rate multiplier
        /// </summary>
        public void SetSpawnRateMultiplier(float multiplier)
        {
            spawnRateMultiplier = Mathf.Clamp(multiplier, 0.1f, 10f);
        }
        
        /// <summary>
        /// Get active generator count
        /// </summary>
        public int GetActiveGeneratorCount()
        {
            return activeGenerators.Count;
        }
        
        /// <summary>
        /// Get total generators killed
        /// </summary>
        public int GetTotalGeneratorsKilled()
        {
            return totalKilled;
        }
        
        /// <summary>
        /// Clear all generators
        /// </summary>
        public void ClearAllGenerators()
        {
            foreach (var gen in activeGenerators)
            {
                if (gen != null)
                {
                    gen.Kill(false);
                }
            }
            activeGenerators.Clear();
        }
        
        /// <summary>
        /// HOOK: Freeze all generators
        /// </summary>
        public void FreezeAllGenerators(float duration)
        {
            foreach (var gen in activeGenerators)
            {
                gen.Freeze(duration);
            }
        }
        
        /// <summary>
        /// HOOK: Slow all generators
        /// </summary>
        public void SlowAllGenerators(float slowFactor, float duration)
        {
            foreach (var gen in activeGenerators)
            {
                gen.Slow(slowFactor, duration);
            }
        }
    }
}
// Removed #endif