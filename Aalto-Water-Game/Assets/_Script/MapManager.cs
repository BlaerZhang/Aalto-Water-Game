using System;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using Random = UnityEngine.Random;
using UnityEngine.UIElements;
using System.Linq;

public class MapManager : MonoBehaviour
{
    #region Constants

    /// <summary>
    /// Number of horizontal tiles on the Map
    /// </summary>
    public int MapWidth = 100;

    /// <summary>
    /// Number of vertical tiles on the Map
    /// </summary>
    public int MapHeight = 100;

    /// <summary>
    /// The Map is updated every *MapUpdateInterval* seconds
    /// </summary>
    [Range(5, 15)] public float MapUpdateInterval = 5f;

    #endregion Constants

    #region Properties 

    /// <summary>
    /// The tile prefab (sprites) to be used
    /// </summary>
    public List<GameObject> TilePrefabList;

    /// <summary>
    /// The building prefab (sprites) to be used
    /// </summary>
    public List<GameObject> BuildingPrefabList;


    /// <summary>
    /// Matrix of Tiles representing the Map.
    /// </summary>
    private Dictionary<Vector2Int, Tile> Map;

    #endregion Properties

    void Start()
    {
        Map = new Dictionary<Vector2Int, Tile>();
        GenerateMap();

        // Update the map periodically according to the Tile's rules
        InvokeRepeating("UpdateMap", MapUpdateInterval, MapUpdateInterval);
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

    /// <summary>
    /// Updates the tiles on the Map according to the parameter passed.
    /// </summary>
    /// <param name="newTilesTypes">{[Key-Coordinates]: [Value-Tile's new Type]}: The key refers to the Tile that should be updated in the Map. The Type refers to the Type that the new tile will have</param>
    /// <param name="baseDelay">Delay in seconds between each tile update (set to 0 to make the change instantaneous).</param>
    private void UpdateTilesRandom(Dictionary<Vector2Int, TileType> newTilesTypes, float baseDelay=0.005f)
    {
        // Convert the dictionary to a list for shuffling
        var tileList = newTilesTypes.ToList();

        // Shuffle the list using Unity's Random class
        for (int i = 0; i < tileList.Count; i++)
        {
            var temp = tileList[i];
            int randomIndex = Random.Range(i, tileList.Count);
            tileList[i] = tileList[randomIndex];
            tileList[randomIndex] = temp;
        }

        // Loop through the shuffled list and update the tiles
        for (int i = 0; i < tileList.Count; i++)
        {
            var kvp = tileList[i];
            Vector2Int position = kvp.Key;
            TileType newTileType = kvp.Value;

            // Introduce a staggered delay for each tile update
            float delay = i * baseDelay; // Adjust the delay to control the stagger effect

            // Scale down, destroy, and replace the tile with a delay
            Map[position].Sprite.transform.DOScale(0, 0.1f).SetDelay(delay).OnComplete(() =>
            {
                // Destroy the existing tile GameObject
                Destroy(Map[position].Sprite);

                // Instantiate the new tile sprite and create the Tile object
                GameObject tileSprite = Instantiate(TilePrefabList[(int)newTileType], Tile.ConvertCoordinatesToIsometric(position), Quaternion.identity);
                tileSprite.transform.localScale = Vector3.zero;
                Tile tile = Tile.CreateTile(newTileType, position, tileSprite);

                // Update the Map dictionary with the new tile
                Map[position] = tile;

                // Scale up the new tile with a smooth animation
                tileSprite.transform.DOScale(1, 0.3f).SetEase(Ease.OutElastic);
            });
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

        UpdateTilesRandom(tilesToChange);
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

    public bool BuildingIsPossibleOnTile(Vector3 tilePosition)
    {
        return true;
    }

    public void PlaceBuilding(Vector3 tilePosition, BuildingType type) 
    {
        var tileKey = Tile.ConvertIsometricToCoordinates(tilePosition);

        // Instantiate the new tile sprite and create the Tile object
        GameObject buildingSprite = Instantiate(BuildingPrefabList[(int)type], Tile.ConvertCoordinatesToIsometric(tileKey), Quaternion.identity);
        var building = Building.CreateBuilding(type, tileKey, buildingSprite);

        // Destroy the existing tile GameObject
        Destroy(Map[tileKey].Sprite);

        Map[tileKey] = building;
    }

    public void RemoveBuilding(Vector3 tilePosition)
    {
        var tileKey = Tile.ConvertIsometricToCoordinates(tilePosition);

        var updateDict = new Dictionary<Vector2Int, TileType>() { { tileKey, TileType.Dirt } };
        UpdateTilesRandom(updateDict);
    }

}
