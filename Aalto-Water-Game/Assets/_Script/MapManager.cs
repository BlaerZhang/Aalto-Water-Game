using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine.UIElements;

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
        int xOffset = MapWidth / 2;
        int yOffset = MapHeight / 2;

        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                // Adjust position so that (0, 0) is in the center of the map
                var position = new Vector2Int(x - xOffset, y - yOffset);

                // Get surrounding tiles
                List<Tile> surroundingTiles = GetSurroundingTiles(position);

                // Decide tile type based on surrounding tiles
                TileType tileType = Tile.GetTileBasedOnSurrounding(surroundingTiles);

                // Instantiate the tile sprite at the calculated position
                GameObject tileSprite = Instantiate(TilePrefabList[(int)tileType], Tile.ConvertCoordinatesToIsometric(position), Quaternion.identity);

                // Create the tile using the selected type and position
                Tile tile = Tile.CreateTile(tileType, position, tileSprite);

                // Store the tile in the map dictionary
                Map[position] = tile;
            }
        }
    }

    private void UpdateTiles(Dictionary<Vector2Int, TileType> newTilesTypes)
    {
        foreach (var position in newTilesTypes.Keys)
        {
            TileType newTileType = newTilesTypes[position];
            GameObject tileSprite = Instantiate(TilePrefabList[(int)newTileType], Tile.ConvertCoordinatesToIsometric(position), Quaternion.identity);
            Tile tile = Tile.CreateTile(newTileType, position, tileSprite);

            Map[position] = tile;
        }
    }

    void UpdateMap()
    {
        int xOffset = MapWidth / 2;
        int yOffset = MapHeight / 2;

        Dictionary<Vector2Int, TileType> tilesToChange = new Dictionary<Vector2Int, TileType>();

        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                var position = new Vector2Int(x - xOffset, y - yOffset);
                var surroundingTiles = GetSurroundingTiles(position);
                TileType tileType = Map[position].ApplyRulesAndGetNewType(surroundingTiles);

                if (tileType != Map[position].Type)
                    tilesToChange[position] = tileType;
            }
        }

        UpdateTiles(tilesToChange);
    }

    private List<Tile> GetSurroundingTiles(Vector2Int position)
    {
        List<Tile> surroundingTiles = new List<Tile>();

        // Define the relative positions of the surrounding tiles
        Vector2Int[] directions = {
        new Vector2Int(-1, 1),  // Top-Left
        new Vector2Int(0, 1),   // Top-Center
        new Vector2Int(1, 1),   // Top-Right
        new Vector2Int(-1, 0),  // Middle-Left
        new Vector2Int(1, 0),   // Middle-Right
        new Vector2Int(-1, -1), // Bottom-Left
        new Vector2Int(0, -1),  // Bottom-Center
        new Vector2Int(1, -1)   // Bottom-Right
    };

        // Collect surrounding tiles based on their positions
        foreach (var direction in directions)
        {
            Vector2Int neighborPos = position + direction;
            if (Map.ContainsKey(neighborPos))
            {
                surroundingTiles.Add(Map[neighborPos]);
            }
            else
            {
                surroundingTiles.Add(null); // Add null if there is no tile
            }
        }

        return surroundingTiles;
    }

    public GameObject GetTile(Vector2Int tilePosition)
    {
        if (Map.TryGetValue(tilePosition, out Tile tile))
            return tile.Sprite;
        return null;
    }
}
