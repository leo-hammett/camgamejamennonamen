using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase interiorTile;
    [SerializeField] private TileBase borderTile;
    [SerializeField] private int width = 20;
    [SerializeField] private int height = 20;
    [SerializeField] private int borderSize = 20;

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        tilemap.ClearAllTiles();

        for (int x = -borderSize; x < width+borderSize; x++)
        {
            for (int y = -borderSize; y < height+borderSize; y++)
            {
                // border if on the edge, otherwise interior
                bool isBorder = x < 0 || x > width || y < 0 || y > height;
                tilemap.SetTile(new Vector3Int(x, y, 0), isBorder ? borderTile : interiorTile);
            }
        }
    }
}
