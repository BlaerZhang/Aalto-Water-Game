using System;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using Random = UnityEngine.Random;
using System.Linq;

public class MapManager : MonoBehaviour
{
    #region Constants

    /// <summary>
    /// Number of horizontal tiles on the Map
    /// </summary>
    public int MapWidth = 50;

    /// <summary>
    /// Number of vertical tiles on the Map
    /// </summary>
    public int MapHeight = 50;

    /// <summary>
    /// The Map is updated every *MapUpdateInterval* seconds
    /// </summary>
    [Range(0.1f, 15)] public float MapUpdateInterval = 1f;

    #region Range Search

    private static readonly List<Vector2Int> RadiusOneSearchArea = new List<Vector2Int>()
    {
        new Vector2Int(-1, 1),  // Top-Left
        new Vector2Int(0, 1),   // Top-Center
        new Vector2Int(1, 1),   // Top-Right
        new Vector2Int(-1, 0),  // Middle-Left
        new Vector2Int(1, 0),   // Middle-Right
        new Vector2Int(-1, -1), // Bottom-Left
        new Vector2Int(0, -1),  // Bottom-Center
        new Vector2Int(1, -1)   // Bottom-Right
    };

    private static readonly List<Vector2Int> RadiusTwoSearchArea = new List<Vector2Int>()
    {
        new Vector2Int(-1, 1),  // Top-Left
        new Vector2Int(0, 1),   // Top-Center
        new Vector2Int(1, 1),   // Top-Right
        new Vector2Int(-1, 0),  // Middle-Left
        new Vector2Int(1, 0),   // Middle-Right
        new Vector2Int(-1, -1), // Bottom-Left
        new Vector2Int(0, -1),  // Bottom-Center
        new Vector2Int(1, -1),  // Bottom-Right
        new Vector2Int(0, 2),   // Top-Center
        new Vector2Int(2, 0),   // Top-Right
        new Vector2Int(-2, 0),  // Middle-Left
        new Vector2Int(0, -2)   // Middle-Right
    };

    // Define the relative positions of the surrounding tiles based on a radius
    private Dictionary<int, List<Vector2Int>> RangeByRadius = new Dictionary<int, List<Vector2Int>>()
    {
        {1, RadiusOneSearchArea },
        {2, RadiusTwoSearchArea }
    };

    public Dictionary<BuildingType, int> BuildingPrices = new Dictionary<BuildingType, int>()
    {
        { BuildingType.Dessalinator, 30 },
        { BuildingType.Reservoir, 20 },
        { BuildingType.Sprinkler, 10 },
    };
    
    #endregion Range Search

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
    public Dictionary<Vector2Int, Tile> Map;

    public AudioClip RemoveBuildingSound;

    #endregion Properties

    #region Unity Methods

    private void Awake()
    {
        
    }

    void Start()
    {
        // GameManager.Instance.MapManager = this;
        // GenerateMap();
        // GenerateMapFromDictionary();

        // Update the map periodically according to the Tile's rules
        InvokeRepeating("UpdateMap", MapUpdateInterval, MapUpdateInterval);
    }

    #endregion Unity Methods

    #region Map Creation

    private Dictionary<Vector2Int, TileType> CenterMapCoordinates(Dictionary<Vector2Int, TileType> originalMapDictionary)
    {
        // Step 1: Calculate the map dimensions
        int mapWidth = originalMapDictionary.Keys.Max(key => key.x) + 1;
        int mapHeight = originalMapDictionary.Keys.Max(key => key.y) + 1;

        // Step 2: Calculate the offset to center the map
        int offsetX = mapWidth / 2;
        int offsetY = mapHeight / 2;

        // Step 3: Create a new dictionary with centered coordinates
        var centeredMapDict = new Dictionary<Vector2Int, TileType>();

        foreach (var kvp in originalMapDictionary)
        {
            // Original position
            Vector2Int originalPosition = kvp.Key;

            // Step 4: Apply the offset to shift the coordinates
            Vector2Int centeredPosition = new Vector2Int(originalPosition.x - offsetX, originalPosition.y - offsetY);

            // Add the new centered position to the new dictionary
            centeredMapDict[centeredPosition] = kvp.Value;
        }

        // Step 5: Replace the original dictionary with the centered one
        return centeredMapDict;
    }

    public void GenerateMap()
    {
        //Destroy all children
        foreach (Transform child in transform) 
        {
            Destroy(child.gameObject);
        }
        
        int xOffset = MapWidth / 2;
        int yOffset = MapHeight / 2;

        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                // Adjust position so that (0, 0) is in the center of the map
                var position = new Vector2Int(x - xOffset, y - yOffset);

                // Get surrounding tiles
                // List<Tile> surroundingTiles = GetSurroundingTiles(position);

                // Decide tile type based on surrounding tiles
                TileType tileType = TileType.SaltyWater; // Tile.GetTileBasedOnSurrounding(surroundingTiles);

                // Instantiate the tile sprite at the calculated position
                GameObject tileSprite = Instantiate(TilePrefabList[(int)tileType], Tile.ConvertCoordinatesToIsometric(position), Quaternion.identity);
                
                //Nest
                tileSprite.transform.parent = this.transform;

                // Create the tile using the selected type and position
                Tile tile = Tile.CreateTile(tileType, position, tileSprite);

                // Store the tile in the map dictionary
                Map[position] = tile;
            }
        }
    }
    
    public void GenerateMapFromDictionary(Dictionary<Vector2Int, TileType> mapAsDictionary, int mapHeight, int mapWidth)
    {
        MapHeight = mapHeight;
        MapWidth = mapWidth;

        var centeredMapAsDictionary = CenterMapCoordinates(mapAsDictionary);

        UpdateTilesRandom(centeredMapAsDictionary, 0.01f);
    }

    #endregion Map Creation

    #region Elements Cration, Destruction and Alteration

    private void CreateNewTile(TileType type, Vector2Int position, out GameObject tileSprite, bool destroyPreviousTile = true)
    {
        // Destroy the existing tile GameObject
        if (destroyPreviousTile) Map[position].Destroy();

        // Instantiate the new tile sprite and create the Tile object
        tileSprite = Instantiate(TilePrefabList[(int)type], Tile.ConvertCoordinatesToIsometric(position), Quaternion.identity);
        tileSprite.transform.localScale = Vector3.zero;
        
        //Nest
        tileSprite.transform.parent = this.transform;

        Tile tile = Tile.CreateTile(type, position, tileSprite);

        // Update the Map dictionary with the new tile
        Map[position] = tile;
        
        //Update Goal
        if (type == GameManager.Instance.LevelManager.CurrentLevelInfoSO.RequiredTileType && type != TileType.Building) 
            GameManager.Instance.LevelManager.CurrentTileNumber += 1;
    }

    public bool BuildingIsPossibleOnTile(Vector3 tilePosition)
    {
        Vector2Int tileKey = Tile.ConvertIsometricToCoordinates(tilePosition);

        return (
            Map.TryGetValue(tileKey, out Tile tile) 
            && (tile.Type != TileType.Building && tile.Type != TileType.SaltyWater && tile.Type != TileType.Rock)
            && GameManager.Instance.LevelManager.CurrentResource - BuildingPrices[GameManager.Instance.UIManager.CurrentBuildingType] >= 0
        );
    }

    public void PlaceBuilding(Vector3 tilePosition)
    {
        var tileKey = Tile.ConvertIsometricToCoordinates(tilePosition);
        var buildingType = GameManager.Instance.UIManager.CurrentBuildingType;

        GameObject dirtSprite = Map[tileKey].Sprite;
        if (Map[tileKey].Type != TileType.Dirt)
        {
            dirtSprite = Instantiate(TilePrefabList[(int)TileType.Dirt], tilePosition, Quaternion.identity);
            Destroy(Map[tileKey].Sprite);  // Destroy the existing old sprite
            
            //Nest
            dirtSprite.transform.parent = this.transform;
        }

        GameObject buildingSprite = Instantiate(BuildingPrefabList[(int)buildingType], tilePosition, Quaternion.identity);
        
        //Nest
        buildingSprite.transform.parent = this.transform;
        
        var building = Building.CreateBuilding(buildingType, tileKey, buildingSprite, dirtSprite);
        Map[tileKey] = building;
        
        //Cost
        GameManager.Instance.LevelManager.CurrentResource -= BuildingPrices[GameManager.Instance.UIManager.CurrentBuildingType];
        
        //Update Goal
        if (GameManager.Instance.LevelManager.CurrentLevelInfoSO.RequiredTileType == TileType.Building
            && GameManager.Instance.LevelManager.CurrentLevelInfoSO.RequiredBuildingTypeIfRequiringBuilding == buildingType
            && building.IsFunctional(GetSurroundingTiles(tileKey, Map[tileKey].SurroundingRadius))
           )
            GameManager.Instance.LevelManager.CurrentTileNumber += 1;
    }

    public void RemoveBuilding(Vector3 tileIsometricPosition)
    {
        var tilePosition = Tile.ConvertIsometricToCoordinates(tileIsometricPosition);
        if (Map[tilePosition].Type != TileType.Building) return;

        GameManager.Instance.AudioManager.PlaySound(RemoveBuildingSound);
        CreateNewTile(TileType.Dirt, tilePosition, out GameObject tileSprite);
    }

    #endregion Elements Cration, Destruction and Alteration

    #region Map Manipulation

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

            if (newTileType == TileType.SaltyWater) continue;

            // Introduce a staggered delay for each tile update
            float delay = i * baseDelay; // Adjust the delay to control the stagger effect

            if (Map.TryGetValue(position, out Tile tile))
            {
                // Scale down, destroy, and replace the tile with a delay
                tile.Sprite.transform.DOScale(0, 0.1f).SetDelay(delay).OnComplete(() =>
                {
                    CreateNewTile(newTileType, position, out GameObject tileSprite);

                    // Scale up the new tile with a smooth animation
                    tileSprite.transform.DOScale(1, 0.3f).SetEase(Ease.OutElastic);
                });
            }
            else
                CreateNewTile(newTileType, position, out GameObject tileSprite, false);
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
                TileType tileType = TileType.Dirt;
                if (Map.TryGetValue(position, out Tile tile) && tile.Type != TileType.SaltyWater)
                {
                    var surroundingTiles = GetSurroundingTiles(position, tile.SurroundingRadius);
                    tile.Update(surroundingTiles, out tileType);
                }
                else
                    continue;

                if (tileType != tile.Type)
                    tilesToChange[position] = tileType;
            }
        }

        UpdateTilesRandom(tilesToChange);
    }

    private List<Tile> GetSurroundingTiles(Vector2Int position, int radius=1)
    {
        List<Tile> surroundingTiles = new List<Tile>();

        // Collect surrounding tiles based on their positions
        foreach (var direction in RangeByRadius[radius])
        {
            Vector2Int neighborPos = position + direction;
            surroundingTiles.Add(Map[neighborPos]);
        }

        return surroundingTiles;
    }
    
    #endregion Map Manipulation
}