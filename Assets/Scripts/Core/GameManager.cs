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
        
        private GameState currentState = GameState.Menu;
        private int currentScore = 0;
        private float timeSurvived = 0f;
        
        private IMapSystem mapSystem;
        private IGeneratorManager generatorManager;
        private IPlayerController playerController;
        
        /// <summary>
        /// Initialize the game and all systems
        /// </summary>
        public void StartGame()
        {
            currentState = GameState.Playing;
            currentScore = 0;
            timeSurvived = 0f;
            
            // TODO: Initialize all systems
        }
        
        /// <summary>
        /// Pause the game
        /// </summary>
        public void PauseGame()
        {
            if (currentState == GameState.Playing)
            {
                currentState = GameState.Paused;
                Time.timeScale = 0f;
            }
        }
        
        /// <summary>
        /// Resume the game
        /// </summary>
        public void ResumeGame()
        {
            if (currentState == GameState.Paused)
            {
                currentState = GameState.Playing;
                Time.timeScale = 1f;
            }
        }
        
        /// <summary>
        /// End the game (player hit gravestone)
        /// </summary>
        public void EndGame()
        {
            currentState = GameState.GameOver;
            Time.timeScale = 0f;
            // TODO: Show game over screen
        }
        
        /// <summary>
        /// Restart the game
        /// </summary>
        public void RestartGame()
        {
            Time.timeScale = 1f;
            StartGame();
        }
        
        /// <summary>
        /// Get current game state
        /// </summary>
        public GameState GetGameState()
        {
            return currentState;
        }
        
        /// <summary>
        /// Get current score
        /// </summary>
        public int GetScore()
        {
            return currentScore;
        }
        
        /// <summary>
        /// Add score for an action
        /// </summary>
        /// <param name="points">Points to add</param>
        /// <param name="reason">Reason for score (for display/analytics)</param>
        public void AddScore(int points, string reason)
        {
            currentScore += points;
            Debug.Log($"Score added: {points} for {reason}. Total: {currentScore}");
        }
        
        /// <summary>
        /// Get time survived
        /// </summary>
        public float GetTimeSurvived()
        {
            return timeSurvived;
        }
        
        /// <summary>
        /// Register a generator kill (for scoring and tracking)
        /// </summary>
        /// <param name="position">Position where generator was killed</param>
        public void OnGeneratorKilled(Vector2 position)
        {
            if (gameConfig != null)
            {
                AddScore(gameConfig.pointsPerGeneratorKill, "Generator Kill");
            }
        }
        
        void Start()
        {
            // Auto-start the game for testing
            StartGame();
            
            // TODO: Future - Add menu system
        }
        
        void Update()
        {
            if (currentState != GameState.Playing) return;
            
            timeSurvived += Time.deltaTime;
            
            if (useOldScripts)
            {
                // Check game over with old scripts
                if (playerMovement != null && !playerMovement.enabled)
                {
                    EndGame();
                }
                
                // Update generator spawning
                if (generatorManager != null)
                {
                    generatorManager.UpdateSystem(Time.deltaTime);
                }
            }
            else
            {
                // TODO: Future - Update all systems
                // mapSystem?.UpdateSystem(Time.deltaTime);
                // generatorManager?.UpdateSystem(Time.deltaTime);
                // playerController?.UpdateSystem(Time.deltaTime);
            }
        }
        
        /// <summary>
        /// HOOK: Future powerup collection
        /// </summary>
        /// <param name="powerupType">Type identifier for powerup</param>
        public void OnPowerupCollected(string powerupType)
        {
            // TODO: Implement powerup collection
            Debug.Log($"Powerup collected: {powerupType}");
        }
        
        /// <summary>
        /// Get reference to map system
        /// </summary>
        public IMapSystem GetMapSystem()
        {
            return mapSystem;
        }
        
        /// <summary>
        /// Get reference to generator manager
        /// </summary>
        public IGeneratorManager GetGeneratorManager()
        {
            return generatorManager;
        }
        
        /// <summary>
        /// Get reference to player controller
        /// </summary>
        public IPlayerController GetPlayerController()
        {
            return playerController;
        }
        
        void Update()
        {
            if (currentState == GameState.Playing)
            {
                timeSurvived += Time.deltaTime;
                
                if (gameConfig != null)
                {
                    AddScore(Mathf.FloorToInt(gameConfig.pointsPerSecond * Time.deltaTime), "Survival");
                }
            }
        }
    }
    
    public enum GameState
    {
        Menu,
        Playing,
        Paused,
        GameOver
    }
}
