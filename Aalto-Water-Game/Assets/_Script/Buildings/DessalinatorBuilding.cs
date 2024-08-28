using System.Collections.Generic;
using UnityEngine;


public class DessalinatorBuilding : Building
{
    public DessalinatorBuilding(Vector2Int tilePosition, GameObject buildingSprite, GameObject dirtTileSprite) 
        : base(BuildingType.Dessalinator, tilePosition, buildingSprite, dirtTileSprite) {}

    public bool CanBuildAccordingToRules(List<Tile> surroundingTiles)
    {
        var tileTypesCount = CountTypes(surroundingTiles);
        return tileTypesCount[TileType.SaltyWater] > 0;
    }
}

