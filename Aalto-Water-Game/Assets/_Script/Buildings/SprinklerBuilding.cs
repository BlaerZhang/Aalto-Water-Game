using UnityEngine;

public class SprinklerBuilding : Building
{
    public SprinklerBuilding(Vector2Int tilePosition, GameObject sprite) : base(BuildingType.Dessalinator, tilePosition, sprite) { }
}