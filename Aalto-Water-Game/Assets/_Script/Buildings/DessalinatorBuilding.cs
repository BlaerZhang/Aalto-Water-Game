﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DessalinatorBuilding : Building
{
    #region Properties

    /// <summary>
    /// Minimum amount of sprinkler that can be powered by the Dessalinator.
    /// </summary>
    private static int MinSprinklerPoweringCapacity = 2;

    /// <summary>
    /// Amount of water produced via Dessalinization. It is calculated taking into acount the amount of water used by the sprinkler.
    /// </summary>
    private static float DessalinizationSpeed = SprinklerBuilding.WaterQuantityToSupply * MinSprinklerPoweringCapacity + 5;

    /// <summary>
    /// Maximum amount of water that the Dessalinator can hold.
    /// </summary>
    public static float MaxCapacity = SprinklerBuilding.WaterQuantityToSupply * 2.5f;

    /// <summary>
    /// Quantity of water currently hold by the Dessalinator.
    /// </summary>
    public float StoredWaterQuantity { get => _storedWaterQuantity; set { _storedWaterQuantity = Mathf.Clamp(value, 0, MaxCapacity); } }
    private float _storedWaterQuantity = 0;

    public static int BuildingPrice = 30;

    #endregion Properties

    public DessalinatorBuilding(Vector2Int tilePosition, GameObject buildingSprite, GameObject dirtTileSprite) 
        : base(BuildingType.Dessalinator, tilePosition, buildingSprite, dirtTileSprite) {}

    public override bool IsFunctional(List<Tile> surroundingTiles)
    {
        return surroundingTiles.Any(tile => tile.Type == TileType.SaltyWater);
    }

    public override void Update(List<Tile> surroundingTiles, out TileType newType)
    {
        // Debug.Log($"Dessalinator Surrounding Count: {surroundingTiles.Count}");
        newType = Type;
        BuildingSprite.GetComponentInChildren<Animator>().SetBool("isActive", IsFunctional(surroundingTiles));
        if (!IsFunctional(surroundingTiles)) return;

        // Debug.Log($"Dessalinator Works and is Creating Water: {DessalinizationSpeed} | Stored: {StoredWaterQuantity}");
        StoredWaterQuantity += DessalinizationSpeed;
    }
}

