using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(CanvasScaler))]
public class SimpleUIScaler : MonoBehaviour
{
    void Awake()
    {
        // Get or add CanvasScaler component
        CanvasScaler scaler = GetComponent<CanvasScaler>();
        
        // Set to scale with screen size
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        
        // Reference resolution (design resolution)
        scaler.referenceResolution = new Vector2(1920, 1080);
        
        // Match width/height equally (0.5 = balanced)
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        scaler.matchWidthOrHeight = 0.5f;
        
        // Ensure Canvas is set to Screen Space - Overlay
        Canvas canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        
        // Fix button positions if they exist
        FixButtonAnchors();
    }
    
    void FixButtonAnchors()
    {
        // Find all buttons in children
        Button[] buttons = GetComponentsInChildren<Button>(true);
        
        foreach (Button btn in buttons)
        {
            RectTransform rect = btn.GetComponent<RectTransform>();
            
            // Center buttons and use relative positioning
            if (btn.name.ToLower().Contains("play") || btn.name.ToLower().Contains("retry"))
            {
                // Center anchor
                rect.anchorMin = new Vector2(0.5f, 0.5f);
                rect.anchorMax = new Vector2(0.5f, 0.5f);
                rect.pivot = new Vector2(0.5f, 0.5f);
                
                // Set size if too small or too large
                if (rect.sizeDelta.x < 200 || rect.sizeDelta.x > 1000)
                {
                    rect.sizeDelta = new Vector2(300, 100);
                }
            }
        }
        
        // Fix text elements
        TMPro.TextMeshProUGUI[] texts = GetComponentsInChildren<TMPro.TextMeshProUGUI>(true);
        foreach (var text in texts)
        {
            RectTransform rect = text.GetComponent<RectTransform>();
            
            // Score text at top
            if (text.name.ToLower().Contains("score"))
            {
                rect.anchorMin = new Vector2(0.5f, 0.9f);
                rect.anchorMax = new Vector2(0.5f, 0.9f);
                rect.pivot = new Vector2(0.5f, 0.5f);
                rect.anchoredPosition = new Vector2(0, -50);
            }
        }
        
        // Fix logo/images
        Image[] images = GetComponentsInChildren<Image>(true);
        foreach (var img in images)
        {
            if (img.name.ToLower().Contains("logo"))
            {
                RectTransform rect = img.GetComponent<RectTransform>();
                rect.anchorMin = new Vector2(0.5f, 0.7f);
                rect.anchorMax = new Vector2(0.5f, 0.7f);
                rect.pivot = new Vector2(0.5f, 0.5f);
                
                // Maintain aspect ratio
                if (img.sprite != null)
                {
                    float aspect = img.sprite.rect.width / img.sprite.rect.height;
                    rect.sizeDelta = new Vector2(400 * aspect, 400);
                }
            }
        }
    }
}