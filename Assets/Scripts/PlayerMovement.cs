using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    // minimum distance the player must travel before a new point is added to the line,
    // prevents adding hundreds of nearly identical points every frame
    [SerializeField] private float minPointDistance = 0.1f;
    [SerializeField] private Color lineColor = Color.white;
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float lineWidth = 0.1f;
    public event System.Action<List<Vector2>> OnLoopClosed;

    // pair each TileBase with a TileData asset to define its speed multiplier
    private List<TileData> tileDataList;
    private Tilemap tilemap;
    private Dictionary<TileBase, TileData> tileDataMap;
    public static float stoneIntensity = 1f; // 1 = normal, 0 = full stone
    private ScreenEffectsManager screenEffects;

    private LineRenderer lineRenderer;
    // stores the world positions of the current line so we can check for self-intersection
    private List<Vector2> points = new List<Vector2>();
    private GameSettings gameSettings;
    private MenuUIController menu;

    [Header("Sprites")]
    public Sprite[] frame1Sprites = new Sprite[8];
    public Sprite[] frame2Sprites = new Sprite[8];

    [Header("Animation")]
    public float animationSpeed = 0.15f;

    [Header("Effects")]
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private int deathParticleCount = 20;

    private SpriteRenderer spriteRenderer;
    private float animationTimer = 0f;
    private bool useFrame2 = false;
    private int currentDirectionIndex = 0;

    void Awake()
    {
        tileDataList = Resources.Load<TileDictionary>("TileDictionary").tileDataList;
        gameSettings = Resources.Load<GameSettings>("GameSettings");
        tilemap = FindFirstObjectByType<Tilemap>();
        menu = FindFirstObjectByType<MenuUIController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        screenEffects = FindFirstObjectByType<ScreenEffectsManager>();
        if (screenEffects == null)
        {
            GameObject effectsGO = new GameObject("ScreenEffectsManager");
            screenEffects = effectsGO.AddComponent<ScreenEffectsManager>();
        }
        
        // Ensure indicator manager exists
        if (FindFirstObjectByType<EnemyIndicatorManager>() == null)
        {
            GameObject indicatorGO = new GameObject("EnemyIndicatorManager");
            indicatorGO.AddComponent<EnemyIndicatorManager>();
        }
    }

    void Start()
    {
        transform.position = new Vector3(gameSettings.width/4, gameSettings.height/4, 0);

        // build lookup dictionary from the two parallel lists
        tileDataMap = new Dictionary<TileBase, TileData>();
        for (int i = 0; i < tileDataList.Count; i++)
            tileDataMap[tileDataList[i].tile] = tileDataList[i];
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.widthMultiplier = lineWidth;
        lineRenderer.widthCurve = AnimationCurve.Constant(0, 1, 1);
        menu.StartGame += OnGameStart;
        menu.Died += OnDeath;
        ValidateSpriteArrays();
    }

    void OnGameStart()
    {
        if (deathParticles != null)
        {
            deathParticles.Clear();
        }
        spriteRenderer.enabled = true;
        transform.position = new Vector3(gameSettings.width/4, gameSettings.height/4, 0);
        stoneIntensity = 1f; // Reset stone intensity
        StartNewLine();
    }

    void OnDeath()
    {
        spriteRenderer.enabled = false;
        if (deathParticles != null)
        {
            deathParticles.Emit(deathParticleCount);
        }
    }

    void Update()
    {
        // convert mouse screen position to world space using the new Input System
        Vector2 mouseScreen = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = transform.position.z;

        UpdateDirectionIndex(mouseWorld);
        UpdateAnimation();

        if (!menu.playing)
        {
            return;
        }

        // look up the tile at the player's current position
        TileBase currentTile = tilemap.GetTile(tilemap.WorldToCell(transform.position));
        float speedMultiplier = (currentTile != null && tileDataMap.TryGetValue(currentTile, out TileData data)) ? data.speedMultiplier : 1f;
        
        // Update stone intensity (inverse of speed multiplier)
        stoneIntensity = speedMultiplier;
        screenEffects.SetStoneIntensity(stoneIntensity);

        if (speedMultiplier == 0)
        {
            menu.EndGame();
            return;
        }

        // Always move at full speed regardless of tile
        transform.position += (mouseWorld - transform.position).normalized * baseSpeed * Time.deltaTime;

        Vector2 pos = transform.position;

        // always check for self-intersection, regardless of distance from last point.
        // we skip the last 2 segments to avoid false positives from adjacent segments.
        if (points.Count >= 2)
        {
            Vector2 segA = points[points.Count - 1];
            for (int i = 0; i < points.Count - 2; i++)
            {
                if (SegmentsIntersect(segA, pos, points[i], points[i + 1]))
                {
                    OnLoopClosed?.Invoke(new List<Vector2>(points));
                    StartNewLine();
                    return;
                }
            }
        }

        // skip adding a point if we haven't moved far enough yet
        if (Vector2.Distance(pos, points[points.Count - 1]) < minPointDistance)
            return;

        // no intersection, add the new point and extend the line
        points.Add(pos);
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, pos);
    }

    void StartNewLine()
    {
        points.Clear();
        points.Add(transform.position);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
    }

    void UpdateDirectionIndex(Vector2 mouseWorldPosition)
    {
        Vector2 directionVector = mouseWorldPosition - (Vector2)transform.position;
        float angleInDegrees = Mathf.Atan2(directionVector.x, directionVector.y) * Mathf.Rad2Deg;
        if (angleInDegrees < 0f) angleInDegrees += 360f;
        currentDirectionIndex = Mathf.FloorToInt((angleInDegrees + 22.5f) / 45f) % 8;
    }

    void UpdateAnimation()
    {
        Sprite[] currentFrameArray;

        if (menu.playing)
        {
            animationTimer += Time.deltaTime;
            if (animationTimer >= animationSpeed)
            {
                animationTimer = 0f;
                useFrame2 = !useFrame2;
            }
            currentFrameArray = useFrame2 ? frame2Sprites : frame1Sprites;
        }
        else
        {
            currentFrameArray = frame1Sprites;
        }

        if (currentFrameArray != null && currentDirectionIndex < currentFrameArray.Length)
        {
            Sprite targetSprite = currentFrameArray[currentDirectionIndex];
            if (targetSprite != null)
                spriteRenderer.sprite = targetSprite;
        }
    }

    void ValidateSpriteArrays()
    {
        if (frame1Sprites == null || frame1Sprites.Length != 8)
            Debug.LogWarning("PlayerMovement: frame1Sprites should contain exactly 8 sprites.");
        if (frame2Sprites == null || frame2Sprites.Length != 8)
            Debug.LogWarning("PlayerMovement: frame2Sprites should contain exactly 8 sprites.");
    }

    // standard 2D line segment intersection test using cross products
    // t and u are the intersection parameters along each segment (0-1 = on the segment).
    // small epsilon on the bounds (0.01 / 0.99) avoids triggering on shared endpoints
    bool SegmentsIntersect(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2)
    {
        Vector2 d1 = a2 - a1;
        Vector2 d2 = b2 - b1;
        float cross = d1.x * d2.y - d1.y * d2.x;
        if (Mathf.Abs(cross) < 1e-6f) return false; // parallel lines
        Vector2 diff = b1 - a1;
        float t = (diff.x * d2.y - diff.y * d2.x) / cross;
        float u = (diff.x * d1.y - diff.y * d1.x) / cross;
        return t > 0.01f && t < 0.99f && u > 0.01f && u < 0.99f;
    }
}
