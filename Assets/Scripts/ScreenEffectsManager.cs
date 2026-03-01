using UnityEngine;
using UnityEngine.UI;

public class ScreenEffectsManager : MonoBehaviour
{
    private Image redVignette;
    private GameObject vignetteObject;
    
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
        RectTransform rect = vignetteObject.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.sizeDelta = Vector2.zero;
        rect.anchoredPosition = Vector2.zero;
        
        // Create vignette texture (darker at edges)
        Texture2D vignetteTex = new Texture2D(256, 256);
        for (int y = 0; y < 256; y++)
        {
            for (int x = 0; x < 256; x++)
            {
                float distX = Mathf.Abs(x - 128) / 128f;
                float distY = Mathf.Abs(y - 128) / 128f;
                float dist = Mathf.Max(distX, distY);
                float alpha = Mathf.Pow(dist, 2f);
                vignetteTex.SetPixel(x, y, new Color(1f, 1f, 1f, alpha));
            }
        }
        vignetteTex.Apply();
        
        redVignette.sprite = Sprite.Create(vignetteTex, new Rect(0, 0, 256, 256), Vector2.one * 0.5f);
        redVignette.type = Image.Type.Sliced;
        redVignette.raycastTarget = false;
    }
    
    public void SetStoneIntensity(float intensity)
    {
        if (redVignette != null)
        {
            // Inverse the intensity (1 - intensity) because speedMultiplier 0 = full stone
            float actualIntensity = 1f - intensity;
            redVignette.color = new Color(1f, 0f, 0f, actualIntensity * 0.5f);
        }
    }
}