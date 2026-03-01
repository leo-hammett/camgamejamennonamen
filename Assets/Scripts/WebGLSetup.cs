using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WebGLSetup : MonoBehaviour
{
    void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        // WebGL-specific optimizations
        Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 1;
        
        // Reduce quality for better WebGL performance
        QualitySettings.pixelLightCount = 1;
        QualitySettings.antiAliasing = 0;
        QualitySettings.shadows = UnityEngine.ShadowQuality.Disable;
        
        // Ensure textures use appropriate compression
        QualitySettings.globalTextureMipmapLimit = 0;
        
        Debug.Log("WebGL optimizations applied");
#endif
        
        // Verify critical resources are loaded
        VerifyResources();
    }
    
    void VerifyResources()
    {
        // Check TileDictionary
        var tileDict = Resources.Load<TileDictionary>("TileDictionary");
        if (tileDict == null)
        {
            Debug.LogError("TileDictionary not found in Resources!");
        }
        else
        {
            Debug.Log($"TileDictionary loaded with {tileDict.tileDataList?.Count ?? 0} tiles");
        }
        
        // Check GameSettings
        var gameSettings = Resources.Load<GameSettings>("GameSettings");
        if (gameSettings == null)
        {
            Debug.LogError("GameSettings not found in Resources!");
        }
        else
        {
            Debug.Log("GameSettings loaded successfully");
        }
        
        // Check StoneGrower prefab
        var stoneGrower = Resources.Load<GameObject>("StoneGrower");
        if (stoneGrower == null)
        {
            Debug.LogError("StoneGrower prefab not found in Resources!");
        }
        else
        {
            Debug.Log("StoneGrower prefab loaded successfully");
        }
    }
}