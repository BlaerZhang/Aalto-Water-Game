using UnityEngine;

internal class ReservoirBuilding : Building
{
    public ReservoirBuilding(Vector2Int tilePosition, GameObject buildingSprite, GameObject dirtTileSprite)
        : base(BuildingType.Reservoir, tilePosition, buildingSprite, dirtTileSprite) { }
}