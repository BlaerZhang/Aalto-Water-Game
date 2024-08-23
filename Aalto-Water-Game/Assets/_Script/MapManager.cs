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
    private Dictionary<Vector2Int, GameObject> Map;


    /// <summary>
    /// Initializes the mapping from TileType to the corresponding tile prefab dynamically using custom attributes.
    /// </summary>
    void InitializeTileTypeToPrefabMapping()
    {
        TileTypeToPrefab = new Dictionary<TileType, GameObject>();

        foreach (TileType tileType in Enum.GetValues(typeof(TileType)))
        {
            var attribute = (TileType)tileType.GetType()
                .GetField(tileType.ToString())
                .GetCustomAttributes(typeof(TilePrefabIndexAttribute), false)
                .FirstOrDefault();

            if (attribute == null)
            {
                Debug.LogError($"No TilePrefabIndexAttribute found for TileType: {tileType}");
                return;
            }

            int prefabIndex = attribute.PrefabIndex;
            if (prefabIndex >= 0 && prefabIndex < TilePrefabList.Count)
            {
                TileType[tileType] = TilePrefabList[prefabIndex];
            }
            else
            {
                Debug.LogError($"Prefab index out of bounds for TileType: {tileType}");
            }
        }
    }

    void Start()
    {
        tileMap = new Dictionary<Vector2Int, GameObject>();
        InitiateTileTypesDictionary();
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                Tile tile = CreateTile(tilePrefabList[Random.Range(0, 2)], Vector2Int(x, y));
                tileMap[tilePosition] = tile;
            }
        }
    }

    public GameObject GetTile(Vector2Int tilePosition)
    {
        if (tileMap.TryGetValue(tilePosition, out GameObject tile))
            return tile;
        return null;
    }
}
