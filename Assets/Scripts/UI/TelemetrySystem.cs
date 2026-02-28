using UnityEngine;
using UnityEngine.UI;

namespace InGraved.UI
{
    /// <summary>
    /// Concrete implementation of textless telemetry system
    /// </summary>
    public class TelemetrySystem : MonoBehaviour, ITelemetrySystem
    {
        [Header("Screen Edge Glow")]
        public Image screenEdgeGlow;
        public Gradient dangerGradient;
        
        [Header("Effect Prefabs")]
        public GameObject pulsePrefab;
        public GameObject spawnWarningPrefab;
        public GameObject encirclementSuccessPrefab;
        
        public bool IsReady { get; private set; }
        
        /// <summary>
        /// Initialize system
        /// </summary>
        public void Initialize();
        
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
        /// Update player danger telemetry
        /// </summary>
        public void UpdatePlayerTelemetry(Vector2 playerPosition, float stoneStrength);
        
        /// <summary>
        /// Set danger visualization level
        /// </summary>
        public void SetDangerLevel(float intensity);
        
        /// <summary>
        /// Flash screen edge effect
        /// </summary>
        public void FlashScreenEdge(Color color, float duration);
        
        /// <summary>
        /// Show pulse at position
        /// </summary>
        public void ShowPulseEffect(Vector2 worldPosition, Color color, float radius);
        
        /// <summary>
        /// Show spawn warning effect
        /// </summary>
        public void ShowSpawnWarning(Vector2 spawnPosition);
        
        /// <summary>
        /// Show successful encirclement
        /// </summary>
        public void ShowEncirclementSuccess(Bounds encirclementArea);
        
        /// <summary>
        /// Update score visualization
        /// </summary>
        public void UpdateScoreVisual(int score, float multiplier);
        
        /// <summary>
        /// Show game over effects
        /// </summary>
        public void ShowGameOverEffect();
        
        /// <summary>
        /// Reset all effects
        /// </summary>
        public void ResetEffects();
        
        /// <summary>
        /// Show powerup collection
        /// </summary>
        public void ShowPowerupEffect(string powerupType, Vector2 position);
    }
}