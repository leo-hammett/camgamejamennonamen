using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Helper script to quickly set up the InGraved game scene
/// Attach this to an empty GameObject and click "Auto Setup Scene" in the Inspector
/// </summary>
public class SetupHelper : MonoBehaviour
{
    [Header("Drag Config Assets Here")]
    public Object gameConfig;
    public Object mapConfig; 
    public Object generatorConfig;
    public Object playerConfig;
    
    [Header("Scene References")]
    public GameObject playerObject;
    public Tilemap tilemap;
    
    [Header("Click to Setup")]
    [Space(10)]
    [InspectorButton("AutoSetupScene")]
    public bool setupButton;
    
    public void AutoSetupScene()
    {
        Debug.Log("=== Starting InGraved Scene Setup ===");
        
        // Find or create GameManager
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager == null)
        {
            gameManager = new GameObject("GameManager");
            Debug.Log("✓ Created GameManager");
        }
        
        // Add GameManager component if needed
        var gmComponent = gameManager.GetComponent("GameManager");
        if (gmComponent == null)
        {
            Debug.LogWarning("GameManager script is disabled with #if false - enable it in code first!");
        }
        
        // Find or create GeneratorManager
        GameObject generatorManager = GameObject.Find("GeneratorManager");
        if (generatorManager == null)
        {
            generatorManager = new GameObject("GeneratorManager");
            Debug.Log("✓ Created GeneratorManager");
        }
        
        // Setup Player
        if (playerObject == null)
        {
            playerObject = GameObject.Find("Circle");
            if (playerObject == null)
            {
                playerObject = GameObject.FindWithTag("Player");
            }
        }
        
        if (playerObject != null)
        {
            // Tag as Player
            playerObject.tag = "Player";
            
            // Add components if needed
            if (playerObject.GetComponent("PlayerController") == null)
            {
                Debug.Log("Note: PlayerController would be added but script is disabled");
            }
            if (playerObject.GetComponent("TrailSystem") == null)
            {
                Debug.Log("Note: TrailSystem would be added but script is disabled");
            }
            
            Debug.Log("✓ Player setup complete");
        }
        else
        {
            Debug.LogWarning("! Could not find player object (Circle)");
        }
        
        // Setup Tilemap
        if (tilemap == null)
        {
            GameObject gridObj = GameObject.Find("Grid");
            if (gridObj != null)
            {
                tilemap = gridObj.GetComponentInChildren<Tilemap>();
            }
        }
        
        if (tilemap != null)
        {
            var mapSystem = tilemap.GetComponent("MapSystem");
            if (mapSystem == null)
            {
                Debug.Log("Note: MapSystem would be added but script is disabled");
            }
            Debug.Log("✓ Tilemap found");
        }
        else
        {
            Debug.LogWarning("! No tilemap found - create with: GameObject > 2D Object > Tilemap > Rectangular");
        }
        
        // Setup Canvas for Telemetry
        Canvas canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObj = new GameObject("Canvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
            canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();
            Debug.Log("✓ Created Canvas");
        }
        
        var telemetry = canvas.GetComponent("TelemetrySystem");
        if (telemetry == null)
        {
            Debug.Log("Note: TelemetrySystem would be added but script is disabled");
        }
        
        Debug.Log("=== Setup Complete ===");
        Debug.Log("Next steps:");
        Debug.Log("1. Remove #if false from the scripts you want to enable");
        Debug.Log("2. Link config assets to their respective managers");
        Debug.Log("3. Create a simple generator prefab (GameObject with StoneGenerator script)");
        Debug.Log("4. Press Play!");
    }
}

// Helper attribute for Inspector button
public class InspectorButtonAttribute : PropertyAttribute
{
    public string MethodName { get; }
    public InspectorButtonAttribute(string methodName)
    {
        MethodName = methodName;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(InspectorButtonAttribute))]
public class InspectorButtonPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        InspectorButtonAttribute buttonAttribute = (InspectorButtonAttribute)attribute;
        
        if (GUI.Button(position, "Auto Setup Scene"))
        {
            var target = property.serializedObject.targetObject;
            var method = target.GetType().GetMethod(buttonAttribute.MethodName);
            method?.Invoke(target, null);
        }
    }
}
#endif