using UnityEngine;

internal class ReservoirBuilding : Building
{
    public ReservoirBuilding(Vector2Int tilePosition, GameObject sprite) : base(BuildingType.Dessalinator, tilePosition, sprite) { }
}