using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Tiles/TileDictionary")]
public class TileDictionary : ScriptableObject
{
    public List<TileData> tileDataList;
}
