using UnityEngine;


public class DessalinatorBuilding : Building
{
    public DessalinatorBuilding(Vector2Int tilePosition, GameObject buildingSprite, GameObject dirtTileSprite) 
        : base(BuildingType.Dessalinator, tilePosition, buildingSprite, dirtTileSprite) {}
}

