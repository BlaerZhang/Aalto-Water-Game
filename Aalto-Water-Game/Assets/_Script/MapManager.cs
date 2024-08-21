using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    public List<GameObject> tilePrefabList; // The tile prefab to be used
    public int mapWidth = 10;
    public int mapHeight = 10;

    private Dictionary<Vector2Int, GameObject> tileMap;

    void Start()
    {
        tileMap = new Dictionary<Vector2Int, GameObject>();
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                Vector2Int tilePosition = new Vector2Int(x, y);
                GameObject tile = Instantiate(tilePrefabList[Random.Range(0,2)], GetTilePosition(tilePosition), Quaternion.identity);
                tileMap[tilePosition] = tile;
            }
        }
    }

    Vector3 GetTilePosition(Vector2Int tilePosition)
    {
        float tileSize = tilePrefabList[Random.Range(0,2)].transform.localScale.x;
        float halfTileSize = tileSize / 2f;
        float x = (tilePosition.x - tilePosition.y) * halfTileSize;
        float y = (tilePosition.x + tilePosition.y) * halfTileSize * 0.5f;
        return new Vector3(x, y, 0f);
    }

    public GameObject GetTile(Vector2Int tilePosition)
    {
        if (tileMap.TryGetValue(tilePosition, out GameObject tile))
            return tile;
        return null;
    }
}
