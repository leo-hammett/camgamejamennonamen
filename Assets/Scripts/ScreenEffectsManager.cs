using UnityEngine;
using UnityEngine.UI;

public class ScreenEffectsManager : MonoBehaviour
{
    private Image redVignette;
    private GameObject vignetteObject;
    private RectTransform vignetteRect;
    
    void Start()
    {
        CreateRedVignette();
    }
    
    void CreateRedVignette()
    {
        // Create canvas if needed
        Canvas canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasGO = new GameObject("EffectsCanvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
        }
        
        // Create vignette image
        vignetteObject = new GameObject("RedVignette");
        vignetteObject.transform.SetParent(canvas.transform, false);
        
        redVignette = vignetteObject.AddComponent<Image>();
        redVignette.color = new Color(1f, 0f, 0f, 0f); // Red, transparent
        
        // Full screen rect
        vignetteRect = vignetteObject.GetComponent<RectTransform>();
        vignetteRect.anchorMin = Vector2.zero;
        vignetteRect.anchorMax = Vector2.one;
        vignetteRect.sizeDelta = Vector2.zero;
        vignetteRect.anchoredPosition = Vector2.zero;
        
        // Create border texture (hollow rectangle)
        CreateBorderTexture();
        
        redVignette.type = Image.Type.Sliced;
        redVignette.raycastTarget = false;
    }
    
    void CreateBorderTexture()
    {
        int texSize = 256;
        Texture2D borderTex = new Texture2D(texSize, texSize);
        int borderWidth = 80; // How thick the edge gradient is
        
        for (int y = 0; y < texSize; y++)
        {
            for (int x = 0; x < texSize; x++)
            {
                // Calculate distance from edge
                int distFromEdge = Mathf.Min(x, y, texSize - 1 - x, texSize - 1 - y);
                
                // Create gradient from edge
                float alpha = 1f;
                if (distFromEdge < borderWidth)
                {
                    alpha = 1f - (float)distFromEdge / borderWidth;
                    alpha = alpha * alpha; // Square for more dramatic falloff
                }
                else
                {
                    alpha = 0f;
                }
                
                borderTex.SetPixel(x, y, new Color(1f, 1f, 1f, alpha));
            }
        }
        borderTex.Apply();
        
        redVignette.sprite = Sprite.Create(borderTex, 
            new Rect(0, 0, texSize, texSize), 
            Vector2.one * 0.5f);
    }
    
    public void SetStoneIntensity(float intensity)
    {
        if (redVignette != null)
        {
            // Inverse intensity (1 = normal, 0 = full stone)
            float stoneLevel = 1f - intensity;
            
            // Death at full stone - turn black
            if (stoneLevel >= 0.999f)
            {
                redVignette.color = new Color(0f, 0f, 0f, 1f); // Black for death
                return;
            }
            
            // Progressive intensity: subtle red edges -> stronger red -> almost black
            if (stoneLevel < 0.1f)
            {
                // No stone - no effect
                redVignette.color = new Color(1f, 0f, 0f, 0f);
            }
            else if (stoneLevel < 0.9f)
            {
                // Red glow that gets stronger
                float alpha = Mathf.Lerp(0.1f, 0.7f, stoneLevel);
                redVignette.color = new Color(1f, 0f, 0f, alpha);
            }
            else
            {
                // Near death - darker red approaching black
                float darkness = (stoneLevel - 0.9f) * 10f; // 0 to 1 in last 10%
                redVignette.color = Color.Lerp(
                    new Color(0.5f, 0f, 0f, 0.8f), // Dark red
                    new Color(0.1f, 0f, 0f, 0.95f), // Almost black
                    darkness
                );
            }
        }
    }
}