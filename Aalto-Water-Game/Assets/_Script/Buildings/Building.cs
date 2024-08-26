using System.Collections.Generic;
using UnityEngine;


public class Building : Tile
{
    public BuildingType BuildingType {get; private set;}

    public Building(BuildingType type, Vector2Int tilePosition, GameObject sprite): base(TileType.Building, tilePosition, sprite)
    {
        BuildingType = type;
    }

    public override TileType ApplyRulesAndGetNewType(List<Tile> surroundingTiles)
    {
        return this.Type;
    }

    public static Building CreateBuilding(BuildingType type, Vector2Int position, GameObject sprite)
    {
        switch (type)
        {
            case BuildingType.Dessalinator:
                return new DessalinatorBuilding(position, sprite);
            case BuildingType.Sprinkler:
                return new SprinklerBuilding(position, sprite);
            case BuildingType.Reservoir:
                return new ReservoirBuilding(position, sprite);
            // Add cases for other building types
            default:
                Debug.Log($"Building type {type.ToString()} does not exist");
                return null;
        }
    }
}

