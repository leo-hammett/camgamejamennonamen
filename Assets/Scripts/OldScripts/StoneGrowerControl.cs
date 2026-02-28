using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class StoneGrowerControl : MonoBehaviour
{
    private PlayerMovement movement;
    [SerializeField] private float speed = 3f;
    private Tilemap tilemap;
    [SerializeField] private TileBase stoneTile;
    [SerializeField] private int stoneRadius = 1;

    void Awake()
    {
        movement = FindFirstObjectByType<PlayerMovement>();
        tilemap = FindFirstObjectByType<Tilemap>();
    }

    void Start()
    {
        movement.OnLoopClosed += HandleLoopClosed;
    }

    void OnDestroy()
    {
        movement.OnLoopClosed -= HandleLoopClosed;
    }

    void Update()
    {
        Vector3 direction = (movement.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        PaintStone();
    }

    void PaintStone()
    {
        // get the tilemap cell the grower is currently on
        Vector3Int center = tilemap.WorldToCell(transform.position);

        // paint all cells within stoneRadius
        for (int x = -stoneRadius; x <= stoneRadius; x++)
        {
            for (int y = -stoneRadius; y <= stoneRadius; y++)
            {
                if (x * x + y * y <= stoneRadius * stoneRadius)
                    tilemap.SetTile(center + new Vector3Int(x, y, 0), stoneTile);
            }
        }
    }

    void HandleLoopClosed(List<Vector2> loopPoints)
    {
        if (IsInsideLoop(transform.position, loopPoints))
            Destroy(gameObject);
    }

    // ray casting algorithm — cast a ray rightward from the point and count
    // how many loop edges it crosses. odd = inside, even = outside.
    bool IsInsideLoop(Vector2 point, List<Vector2> loop)
    {
        int crossings = 0;
        int count = loop.Count;

        for (int i = 0; i < count; i++)
        {
            Vector2 a = loop[i];
            Vector2 b = loop[(i + 1) % count];

            // check if the edge crosses the horizontal ray going right from point
            if ((a.y > point.y) != (b.y > point.y))
            {
                float xIntersect = a.x + (point.y - a.y) / (b.y - a.y) * (b.x - a.x);
                if (point.x < xIntersect)
                    crossings++;
            }
        }

        return crossings % 2 == 1;
    }
}
