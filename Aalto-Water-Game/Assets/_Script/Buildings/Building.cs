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
        return TileType.Building;
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

    public static bool CanBuildOnTile(BuildingType type, List<Tile> surroundingTiles)
    {
        switch (type)
        {
            case BuildingType.Dessalinator:
                return DessalinatorBuilding.CanBuildAccordingToRules(surroundingTiles);
            case BuildingType.Sprinkler:
                return SprinklerBuilding.CanBuildAccordingToRules(surroundingTiles);
            case BuildingType.Reservoir:
                return ReservoirBuilding.CanBuildAccordingToRules(surroundingTiles);
            // Add cases for other building types
            default:
                Debug.Log($"Building type {type.ToString()} does not exist");
                return false;
        }
    }
    
    public override void Destroy()
    {
        // If it is a Dirt Tile it will be recicled by the MapManager when destroying the building
        if (Type == TileType.Dirt)
            Object.Destroy(Sprite);
        Object.Destroy(BuildingSprite);
    }
}

