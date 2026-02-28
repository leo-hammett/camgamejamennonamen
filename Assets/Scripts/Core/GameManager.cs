#if false
using UnityEngine;
using InGraved.Config;
using InGraved.Map;
using InGraved.Generators;
using InGraved.Player;
using InGraved.UI;

namespace InGraved.Core
{
    /// <summary>
    /// Main game manager that coordinates all systems
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Header("Configuration")]
        public GameConfig gameConfig;
        
        /// <summary>
        /// Initialize the game and all systems
        /// </summary>
        public void StartGame();
        
        /// <summary>
        /// Pause the game
        /// </summary>
        public void PauseGame();
        
        /// <summary>
        /// Resume the game
        /// </summary>
        public void ResumeGame();
        
        /// <summary>
        /// End the game (player hit gravestone)
        /// </summary>
        public void EndGame();
        
        /// <summary>
        /// Restart the game
        /// </summary>
        public void RestartGame();
        
        /// <summary>
        /// Get current game state
        /// </summary>
        public GameState GetGameState();
        
        /// <summary>
        /// Get current score
        /// </summary>
        public int GetScore();
        
        /// <summary>
        /// Add score for an action
        /// </summary>
        /// <param name="points">Points to add</param>
        /// <param name="reason">Reason for score (for display/analytics)</param>
        public void AddScore(int points, string reason);
        
        /// <summary>
        /// Get time survived
        /// </summary>
        public float GetTimeSurvived();
        
        /// <summary>
        /// Register a generator kill (for scoring and tracking)
        /// </summary>
        /// <param name="position">Position where generator was killed</param>
        public void OnGeneratorKilled(Vector2 position);
        
        /// <summary>
        /// HOOK: Future powerup collection
        /// </summary>
        /// <param name="powerupType">Type identifier for powerup</param>
        public void OnPowerupCollected(string powerupType);
        
        /// <summary>
        /// Get reference to map system
        /// </summary>
        public IMapSystem GetMapSystem();
        
        /// <summary>
        /// Get reference to generator manager
        /// </summary>
        public IGeneratorManager GetGeneratorManager();
        
        /// <summary>
        /// Get reference to player controller
        /// </summary>
        public IPlayerController GetPlayerController();
    }
    
    public enum GameState
    {
        Menu,
        Playing,
        Paused,
        GameOver
    }
}
#endif
