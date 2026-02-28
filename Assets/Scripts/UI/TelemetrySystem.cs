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
        public void Initialize()
        {
            IsReady = true;
        }
        
        /// <summary>
        /// Update system
        /// </summary>
        public void UpdateSystem(float deltaTime)
        {
            // TODO: Update visual effects
        }
        
        /// <summary>
        /// Shutdown system
        /// </summary>
        public void Shutdown()
        {
            IsReady = false;
            ResetEffects();
        }
        
        /// <summary>
        /// Reset system
        /// </summary>
        public void Reset()
        {
            ResetEffects();
        }
        
        /// <summary>
        /// Update player danger telemetry
        /// </summary>
        public void UpdatePlayerTelemetry(Vector2 playerPosition, float stoneStrength)
        {
            SetDangerLevel(stoneStrength);
        }
        
        /// <summary>
        /// Set danger visualization level
        /// </summary>
        public void SetDangerLevel(float intensity)
        {
            if (screenEdgeGlow == null) return;
            
            intensity = Mathf.Clamp01(intensity);
            if (dangerGradient != null)
            {
                screenEdgeGlow.color = dangerGradient.Evaluate(intensity);
            }
        }
        
        /// <summary>
        /// Flash screen edge effect
        /// </summary>
        public void FlashScreenEdge(Color color, float duration)
        {
            // TODO: Implement flash effect
        }
        
        /// <summary>
        /// Show pulse at position
        /// </summary>
        public void ShowPulseEffect(Vector2 worldPosition, Color color, float radius)
        {
            if (pulsePrefab != null)
            {
                var pulse = Instantiate(pulsePrefab, worldPosition, Quaternion.identity);
                Destroy(pulse, 2f);
            }
        }
        
        /// <summary>
        /// Show spawn warning effect
        /// </summary>
        public void ShowSpawnWarning(Vector2 spawnPosition)
        {
            if (spawnWarningPrefab != null)
            {
                var warning = Instantiate(spawnWarningPrefab, spawnPosition, Quaternion.identity);
                Destroy(warning, 1f);
            }
        }
        
        /// <summary>
        /// Show successful encirclement
        /// </summary>
        public void ShowEncirclementSuccess(Bounds encirclementArea)
        {
            if (encirclementSuccessPrefab != null)
            {
                var effect = Instantiate(encirclementSuccessPrefab, encirclementArea.center, Quaternion.identity);
                effect.transform.localScale = encirclementArea.size;
                Destroy(effect, 1.5f);
            }
        }
        
        /// <summary>
        /// Update score visualization
        /// </summary>
        public void UpdateScoreVisual(int score, float multiplier)
        {
            // TODO: Update visual score representation
        }
        
        /// <summary>
        /// Show game over effects
        /// </summary>
        public void ShowGameOverEffect()
        {
            // TODO: Show game over visual effect
        }
        
        /// <summary>
        /// Reset all effects
        /// </summary>
        public void ResetEffects()
        {
            SetDangerLevel(0f);
        }
        
        /// <summary>
        /// Show powerup collection
        /// </summary>
        public void ShowPowerupEffect(string powerupType, Vector2 position)
        {
            // TODO: Show powerup effect based on type
        }
    }
}