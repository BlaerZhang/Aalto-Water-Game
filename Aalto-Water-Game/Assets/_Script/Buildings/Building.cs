using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Building : Tile
{
    #region Properties

    public BuildingType BuildingType {get; private set;}

    public GameObject BuildingSprite { get; private set;}
    
    /// <summary>
    /// Price for creating the building
    /// </summary>
    public static int BuildingPrice { get; private set; }

    #endregion Properties

    #region Methods

    public Building(BuildingType type, Vector2Int tilePosition, GameObject buildingSprite, GameObject dirtTileSprite)
        : base(TileType.Building, tilePosition, dirtTileSprite)
    {
        BuildingType = type;
        BuildingSprite = buildingSprite;
        GameManager.Instance.AudioManager.PlayTileCreationSound(TileType.Building);
    }

    public override void Update(List<Tile> surroundingTiles, out TileType newType)
    {
        newType = TileType.Building;
    }

    public static Building CreateBuilding(BuildingType type, Vector2Int position, GameObject buildingSprite, GameObject dirtTileSprite)
    {
        switch (type)
        {
            case BuildingType.Dessalinator:
                return new DessalinatorBuilding(position, buildingSprite, dirtTileSprite);
            case BuildingType.Sprinkler:
                return new SprinklerBuilding(position, buildingSprite, dirtTileSprite);
            case BuildingType.Reservoir:
                return new ReservoirBuilding(position, buildingSprite, dirtTileSprite);
            // Add cases for other building types
            default:
                Debug.Log($"Building type {type.ToString()} does not exist");
                return null;
        }
    }

    /// <summary>
    /// Defines if the Building is working according to its surroundings
    /// </summary>
    /// <param name="surroundingTiles"></param>
    public virtual bool IsFunctional(List<Tile> surroundingTiles) { throw new NotImplementedException(); }

    public override void Destroy()
    {
        // If it is a Dirt Tile it will be recicled by the MapManager when destroying the building
        if (Type == TileType.Dirt)
            UnityEngine.Object.Destroy(Sprite);
        UnityEngine.Object.Destroy(BuildingSprite);
        
        if (GameManager.Instance.LevelManager.CurrentLevelInfoSO.RequiredTileType == TileType.Building)
        {
            if (GameManager.Instance.LevelManager.CurrentLevelInfoSO.RequiredBuildingTypeIfRequiringBuilding ==
                BuildingType)
            {
                GameManager.Instance.LevelManager.CurrentTileNumber -= 1;
            }
        }
    }

    #endregion Methods
}

