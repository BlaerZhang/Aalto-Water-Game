using UnityEngine;

public class SprinklerBuilding : Building
{
    public SprinklerBuilding(Vector2Int tilePosition, GameObject buildingSprite, GameObject dirtTileSprite)
        : base(BuildingType.Sprinkler, tilePosition, buildingSprite, dirtTileSprite) { }
}