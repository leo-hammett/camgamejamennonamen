using UnityEngine;

namespace InGraved.Config
{
    [CreateAssetMenu(fileName = "GeneratorConfig", menuName = "InGraved/Generator Configuration", order = 2)]
    public class GeneratorConfig : ScriptableObject
    {
        [Header("Spawning")]
        [Tooltip("Base spawn rate (generators per second)")]
        public float baseSpawnRate = 0.5f;
        
        [Tooltip("Minimum distance from player to spawn")]
        public float minSpawnDistance = 10.0f;
        
        [Tooltip("Maximum distance from player to spawn")]
        public float maxSpawnDistance = 30.0f;
        
        [Tooltip("Maximum number of generators alive at once")]
        public int maxAliveGenerators = 10;
        
        [Header("Movement")]
        [Tooltip("Base movement speed")]
        public float baseSpeed = 2.0f;
        
        [Tooltip("Speed multiplier per unit of stone strength at generator position")]
        public float speedPerStoneStrength = 3.0f;
        
        [Tooltip("How aggressively generators chase the player (0 = no chase, 1 = direct pursuit)")]
        public float chaseAggressiveness = 0.8f;
        
        [Header("Stone Generation")]
        [Tooltip("Initial radius of effect")]
        public float initialRadius = 2.0f;
        
        [Tooltip("Radius growth per second")]
        public float radiusGrowthRate = 0.5f;
        
        [Tooltip("Maximum radius")]
        public float maxRadius = 10.0f;
        
        [Tooltip("Stone strength added per second to tiles in radius")]
        public float strengthPerSecond = 0.1f;
        
        [Tooltip("Falloff curve from center to edge of radius (0 = edge, 1 = center)")]
        public AnimationCurve strengthFalloffCurve;
        
        [Header("Visual")]
        [Tooltip("Generator sprite/prefab")]
        public GameObject generatorPrefab;
        
        [Tooltip("Effect radius indicator prefab")]
        public GameObject radiusIndicatorPrefab;
        
        [Tooltip("Death effect prefab")]
        public GameObject deathEffectPrefab;
        
        [Tooltip("Spawn effect prefab (lightning strike or similar)")]
        public GameObject spawnEffectPrefab;
    }
}