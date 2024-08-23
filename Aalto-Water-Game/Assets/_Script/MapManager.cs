using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    public List<GameObject> TilePrefabList; // The tile prefab to be used
    private Dictionary<TileType, GameObject> TileTypeToPrefab; // Map from TileType to prefab

    /// <summary>
    /// Number of horizontal tiles on the Map
    /// </summary>
    public int MapWidth = 100;

    /// <summary>
    /// Number of vertical tiles on the Map
    /// </summary>
    public int MapHeight = 100;

    /// <summary>
    /// Matrix of Tiles representing the Map.
    /// </summary>
    private Dictionary<Vector2Int, Tile> Map;

    void Start()
    {
        Map = new Dictionary<Vector2Int, Tile>();
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                var position = new Vector2Int(x, y);
                var tileTypeInt = Random.Range(0, 3);
                Tile tile = Tile.CreateTile((TileType)tileTypeInt, position);
                Map[position] = tile;
            }
        }
    }

    public GameObject GetTile(Vector2Int tilePosition)
    {
        if (Map.TryGetValue(tilePosition, out Tile tile))
            return tile.TileSprite;
        return null;
    }
}
