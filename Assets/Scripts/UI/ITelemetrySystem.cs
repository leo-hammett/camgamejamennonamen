using UnityEngine;
using InGraved.Core;

namespace InGraved.UI
{
    /// <summary>
    /// Interface for textless telemetry/feedback system
    /// </summary>
    public interface ITelemetrySystem : IGameSystem
    {
        /// <summary>
        /// Update telemetry based on player state
        /// </summary>
        /// <param name="playerPosition">Current player position</param>
        /// <param name="stoneStrength">Stone strength at player position</param>
        void UpdatePlayerTelemetry(Vector2 playerPosition, float stoneStrength);
        
        /// <summary>
        /// Show danger indicator (screen edge glow, etc)
        /// </summary>
        /// <param name="intensity">Danger intensity (0 = safe, 1 = maximum danger)</param>
        void SetDangerLevel(float intensity);
        
        /// <summary>
        /// Flash screen edge with color
        /// </summary>
        /// <param name="color">Flash color</param>
        /// <param name="duration">Flash duration</param>
        void FlashScreenEdge(Color color, float duration);
        
        /// <summary>
        /// Pulse effect at position
        /// </summary>
        /// <param name="worldPosition">World position for pulse</param>
        /// <param name="color">Pulse color</param>
        /// <param name="radius">Pulse radius</param>
        void ShowPulseEffect(Vector2 worldPosition, Color color, float radius);
        
        /// <summary>
        /// Show generator spawn warning
        /// </summary>
        /// <param name="spawnPosition">Position where generator will spawn</param>
        void ShowSpawnWarning(Vector2 spawnPosition);
        
        /// <summary>
        /// Show encirclement success feedback
        /// </summary>
        /// <param name="encirclementArea">Area that was encircled</param>
        void ShowEncirclementSuccess(Bounds encirclementArea);
        
        /// <summary>
        /// Update score display (visual, not text)
        /// </summary>
        /// <param name="score">Current score</param>
        /// <param name="multiplier">Score multiplier</param>
        void UpdateScoreVisual(int score, float multiplier);
        
        /// <summary>
        /// Show game over effect
        /// </summary>
        void ShowGameOverEffect();
        
        /// <summary>
        /// Reset all visual effects
        /// </summary>
        void ResetEffects();
        
        /// <summary>
        /// HOOK: Show powerup collection effect
        /// </summary>
        /// <param name="powerupType">Type of powerup</param>
        /// <param name="position">Collection position</param>
        void ShowPowerupEffect(string powerupType, Vector2 position);
    }
}