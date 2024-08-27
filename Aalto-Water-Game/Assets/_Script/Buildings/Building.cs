using System.Collections.Generic;
using UnityEngine;


public class Building : Tile
{
    public BuildingType BuildingType {get; private set;}

    public GameObject BuildingSprite { get; private set;}

    public Building(BuildingType type, Vector2Int tilePosition, GameObject buildingSprite, GameObject dirtTileSprite)
        : base(TileType.Building, tilePosition, dirtTileSprite)
    {
        BuildingType = type;
        BuildingSprite = buildingSprite;
    }

    public override TileType ApplyRulesAndGetNewType(List<Tile> surroundingTiles)
    {
        return this.Type;
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

    public override void Destroy()
    {
        base.Destroy();
        Object.Destroy(BuildingSprite);
    }
}

