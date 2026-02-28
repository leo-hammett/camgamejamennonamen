using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tiles/TileData")]
public class TileData : ScriptableObject
{
    [Range(0f, 1f)] public float speedMultiplier = 1f;
    public Tile tile;
}
