using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnemyIndicatorManager : MonoBehaviour
{
    private List<GameObject> indicators = new List<GameObject>();
    private Canvas canvas;
    private Camera mainCamera;
    private PlayerMovement player;
    
    void Start()
    {
        mainCamera = Camera.main;
        player = FindFirstObjectByType<PlayerMovement>();
        SetupCanvas();
    }
    
    void SetupCanvas()
    {
        canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasGO = new GameObject("IndicatorCanvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
        }
    }
    
    void Update()
    {
        if (player == null) return;
        
        // Find all stone growers
        StoneGrowerControl[] enemies = FindObjectsByType<StoneGrowerControl>(FindObjectsSortMode.None);
        
        // Ensure we have enough indicators
        while (indicators.Count < enemies.Length)
        {
            CreateIndicator();
        }
        
        // Update each indicator
        for (int i = 0; i < enemies.Length; i++)
        {
            UpdateIndicator(indicators[i], enemies[i]);
        }
        
        // Hide unused indicators
        for (int i = enemies.Length; i < indicators.Count; i++)
        {
            indicators[i].SetActive(false);
        }
    }
    
    void CreateIndicator()
    {
        GameObject indicator = new GameObject("EnemyIndicator");
        indicator.transform.SetParent(canvas.transform, false);
        
        Image img = indicator.AddComponent<Image>();
        img.color = new Color(1f, 0.5f, 0f, 0.8f); // Orange
        img.raycastTarget = false;
        
        RectTransform rect = indicator.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(20, 40); // Arrow size
        
        // Create simple triangle texture
        Texture2D arrowTex = new Texture2D(32, 64);
        Color clear = new Color(0, 0, 0, 0);
        Color solid = Color.white;
        
        for (int y = 0; y < 64; y++)
        {
            for (int x = 0; x < 32; x++)
            {
                // Simple triangle shape
                int halfWidth = (64 - y) / 4;
                bool isInTriangle = x >= 16 - halfWidth && x <= 16 + halfWidth;
                arrowTex.SetPixel(x, y, isInTriangle ? solid : clear);
            }
        }
        arrowTex.Apply();
        
        img.sprite = Sprite.Create(arrowTex, new Rect(0, 0, 32, 64), new Vector2(0.5f, 0.5f));
        
        indicators.Add(indicator);
    }
    
    void UpdateIndicator(GameObject indicator, StoneGrowerControl enemy)
    {
        if (enemy == null)
        {
            indicator.SetActive(false);
            return;
        }
        
        Vector3 enemyScreenPos = mainCamera.WorldToScreenPoint(enemy.transform.position);
        
        // Check if enemy is on screen
        bool onScreen = enemyScreenPos.z > 0 && 
                       enemyScreenPos.x > 0 && enemyScreenPos.x < Screen.width &&
                       enemyScreenPos.y > 0 && enemyScreenPos.y < Screen.height;
        
        if (onScreen)
        {
            indicator.SetActive(false);
            return;
        }
        
        indicator.SetActive(true);
        
        // Calculate direction from screen center to enemy
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector2 direction = ((Vector2)enemyScreenPos - screenCenter).normalized;
        
        // Find edge intersection
        float margin = 50f; // Distance from edge
        float maxX = Screen.width - margin;
        float maxY = Screen.height - margin;
        
        Vector2 edgePos = screenCenter;
        
        // Simple edge calculation
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Hit left or right edge
            float x = direction.x > 0 ? maxX : margin;
            float t = (x - screenCenter.x) / direction.x;
            edgePos = screenCenter + direction * t;
            edgePos.x = x;
            edgePos.y = Mathf.Clamp(edgePos.y, margin, maxY);
        }
        else
        {
            // Hit top or bottom edge
            float y = direction.y > 0 ? maxY : margin;
            float t = (y - screenCenter.y) / direction.y;
            edgePos = screenCenter + direction * t;
            edgePos.y = y;
            edgePos.x = Mathf.Clamp(edgePos.x, margin, maxX);
        }
        
        // Position indicator
        RectTransform rect = indicator.GetComponent<RectTransform>();
        rect.position = edgePos;
        
        // Rotate to point toward enemy
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rect.rotation = Quaternion.Euler(0, 0, angle);
        
        // Fade based on distance
        float distance = Vector3.Distance(player.transform.position, enemy.transform.position);
        float alpha = Mathf.Clamp01(1f - (distance / 50f));
        Image img = indicator.GetComponent<Image>();
        Color c = img.color;
        c.a = 0.3f + alpha * 0.5f;
        img.color = c;
    }
}