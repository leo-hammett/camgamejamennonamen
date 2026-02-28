#if false
using UnityEngine;

namespace InGraved.Core
{
    /// <summary>
    /// Base interface for all game systems. Provides lifecycle hooks.
    /// </summary>
    public interface IGameSystem
    {
        /// <summary>
        /// Initialize the system with configuration
        /// </summary>
        void Initialize();
        
        /// <summary>
        /// Update the system each frame
        /// </summary>
        void UpdateSystem(float deltaTime);
        
        /// <summary>
        /// Clean up the system on shutdown
        /// </summary>
        void Shutdown();
        
        /// <summary>
        /// Reset the system to initial state
        /// </summary>
        void Reset();
        
        /// <summary>
        /// Check if the system is ready to operate
        /// </summary>
        bool IsReady { get; }
    }
}
#endif
