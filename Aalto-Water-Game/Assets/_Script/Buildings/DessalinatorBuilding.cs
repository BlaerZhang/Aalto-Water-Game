using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DessalinatorBuilding : Building
{

    #region Properties

    /// <summary>
    /// Amount of water produced via Dessalinization
    /// </summary>
    private static float DessalinizationSpeed = 3f;

    /// <summary>
    /// Maximum amount of water that the Dessalinator can hold.
    /// </summary>
    public static float MaxCapacity = 9f;

    /// <summary>
    /// Quantity of water currently hold by the Dessalinator.
    /// </summary>
    public float StoredWaterQuantity { get => _storedWaterQuantity; set { _storedWaterQuantity = Mathf.Clamp(value, 0, MaxCapacity); } }
    private float _storedWaterQuantity = 0;

    #endregion Properties

    public DessalinatorBuilding(Vector2Int tilePosition, GameObject buildingSprite, GameObject dirtTileSprite) 
        : base(BuildingType.Dessalinator, tilePosition, buildingSprite, dirtTileSprite) {}

    public override bool IsFunctional(List<Tile> surroundingTiles)
    {
        return surroundingTiles.Any(tile => tile.Type == TileType.SaltyWater);
    }

    public override void Update(List<Tile> surroundingTiles, out TileType newType)
    {
        Debug.Log($"Dessalinator Surrounding Count: {surroundingTiles.Count}");
        newType = Type;
        if (!IsFunctional(surroundingTiles)) return;

        Debug.Log($"Dessalinator Works and is Creating Water: {DessalinizationSpeed} | Stored: {StoredWaterQuantity}");
        StoredWaterQuantity += DessalinizationSpeed;
    }
}

