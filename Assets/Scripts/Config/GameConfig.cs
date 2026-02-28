#if false
using UnityEngine;

namespace InGraved.Config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "InGraved/Game Configuration", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [Header("Core Configuration References")]
        public MapConfig mapConfig;
        public GeneratorConfig generatorConfig;
        public PlayerConfig playerConfig;
        
        [Header("Difficulty Scaling")]
        [Tooltip("Time in seconds before difficulty starts increasing")]
        public float difficultyGracePeriod = 30.0f;
        
        [Tooltip("How much spawn rate increases per minute")]
        public float spawnRateIncreasePerMinute = 0.1f;
        
        [Tooltip("Maximum spawn rate multiplier")]
        public float maxSpawnRateMultiplier = 3.0f;
        
        [Header("Scoring")]
        [Tooltip("Points per generator killed")]
        public int pointsPerGeneratorKill = 100;
        
        [Tooltip("Points per second survived")]
        public float pointsPerSecond = 1.0f;
        
        [Tooltip("Score multiplier based on active generators")]
        public AnimationCurve scoreMultiplierCurve;
        
        [Header("Game Over")]
        [Tooltip("Delay before showing game over screen")]
        public float gameOverDelay = 1.0f;
        
        [Header("Audio")]
        [Tooltip("Background music volume")]
        public float musicVolume = 0.5f;
        
        [Tooltip("SFX volume")]
        public float sfxVolume = 0.7f;
        
        [Header("Powerup Hooks")]
        [Tooltip("Reserved for future powerup spawn rate")]
        public float powerupSpawnRate = 0.0f;
        
        [Tooltip("Reserved for future powerup duration")]
        public float powerupDuration = 0.0f;
    }
}
#endif
