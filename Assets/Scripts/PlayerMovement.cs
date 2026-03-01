using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    // minimum distance the player must travel before a new point is added to the line,
    // prevents adding hundreds of nearly identical points every frame
    [SerializeField] private float minPointDistance = 0.1f;
    [SerializeField] private Material lineMaterial;
    [SerializeField] private float baseSpeed = 5f;
    public event System.Action<List<Vector2>> OnLoopClosed;

    // pair each TileBase with a TileData asset to define its speed multiplier
    private List<TileData> tileDataList;
    private Tilemap tilemap;
    private Dictionary<TileBase, TileData> tileDataMap;

    private LineRenderer lineRenderer;
    // stores the world positions of the current line so we can check for self-intersection
    private List<Vector2> points = new List<Vector2>();
    private GameSettings gameSettings;
    private MenuUIController menu;

    void Awake()
    {
        tileDataList = Resources.Load<TileDictionary>("TileDictionary").tileDataList;
        gameSettings = Resources.Load<GameSettings>("GameSettings");
        tilemap = FindFirstObjectByType<Tilemap>();
        menu = FindFirstObjectByType<MenuUIController>();
    }

    void Start()
    {
        transform.position = new Vector3(gameSettings.width/4, gameSettings.height/4, 0);
        
        // build lookup dictionary from the two parallel lists
        tileDataMap = new Dictionary<TileBase, TileData>();
        for (int i = 0; i < tileDataList.Count; i++)
            tileDataMap[tileDataList[i].tile] = tileDataList[i];
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        menu.StartGame += OnGameStart;
    }

    void OnGameStart()
    {
        transform.position = new Vector3(gameSettings.width/4, gameSettings.height/4, 0);
        StartNewLine();
    }

    void Update()
    {
        if (!menu.playing)
        {
            return;
        }

        // convert mouse screen position to world space using the new Input System
        Vector2 mouseScreen = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = transform.position.z;

        // look up the tile at the player's current position and apply its speed multiplier
        TileBase currentTile = tilemap.GetTile(tilemap.WorldToCell(transform.position));
        float speedMultiplier = (currentTile != null && tileDataMap.TryGetValue(currentTile, out TileData data)) ? data.speedMultiplier : 1f;

        transform.position += (mouseWorld - transform.position).normalized * baseSpeed * speedMultiplier * Time.deltaTime;

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
