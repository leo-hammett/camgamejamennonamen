using UnityEngine;

namespace InGraved.Config
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "InGraved/Player Configuration", order = 3)]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Movement")]
        [Tooltip("Base movement speed")]
        public float moveSpeed = 5.0f;
        
        [Tooltip("How smoothly the player follows finger input")]
        public float inputSmoothness = 0.8f;
        
        [Tooltip("Minimum distance from finger to start moving")]
        public float minMoveDistance = 0.5f;
        
        [Header("Trail")]
        [Tooltip("Maximum number of trail points")]
        public int maxTrailPoints = 100;
        
        [Tooltip("Minimum distance between trail points")]
        public float minTrailPointDistance = 0.2f;
        
        [Tooltip("Trail width")]
        public float trailWidth = 0.5f;
        
        [Tooltip("Trail color")]
        public Color trailColor = Color.cyan;
        
        [Tooltip("Trail fade duration in seconds")]
        public float trailFadeDuration = 2.0f;
        
        [Header("Collision")]
        [Tooltip("Player collision box size")]
        public Vector2 collisionBoxSize = Vector2.one;
        
        [Tooltip("Check radius for gravestone detection")]
        public float gravestoneCheckRadius = 0.5f;
        
        [Header("Visual")]
        [Tooltip("Player sprite/prefab")]
        public GameObject playerPrefab;
        
        [Tooltip("Trail renderer material")]
        public Material trailMaterial;
    }
}