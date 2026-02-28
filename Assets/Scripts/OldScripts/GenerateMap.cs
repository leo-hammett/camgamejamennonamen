using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase interiorTile;
    [SerializeField] private TileBase borderTile;
    [SerializeField] private int width = 20;
    [SerializeField] private int height = 20;

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        tilemap.ClearAllTiles();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // border if on the edge, otherwise interior
                bool isBorder = x == 0 || x == width - 1 || y == 0 || y == height - 1;
                tilemap.SetTile(new Vector3Int(x, y, 0), isBorder ? borderTile : interiorTile);
            }
        }
    }
}
