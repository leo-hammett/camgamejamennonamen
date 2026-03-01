using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateMap : MonoBehaviour
{
    private Tilemap tilemap;
    [SerializeField] private TileBase interiorTile;
    [SerializeField] private TileBase borderTile;
    [SerializeField] private int borderSize = 20;
    private GameSettings gameSettings;

    void Awake()
    {
        gameSettings = Resources.Load<GameSettings>("GameSettings");
        tilemap = FindFirstObjectByType<Tilemap>();
    }

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        tilemap.ClearAllTiles();

        for (int x = -borderSize; x < gameSettings.width+borderSize; x++)
        {
            for (int y = -borderSize; y < gameSettings.height+borderSize; y++)
            {
                // border if on the edge, otherwise interior
                bool isBorder = x < 0 || x > gameSettings.width || y < 0 || y > gameSettings.height;
                tilemap.SetTile(new Vector3Int(x, y, 0), isBorder ? borderTile : interiorTile);
            }
        }
    }
}
