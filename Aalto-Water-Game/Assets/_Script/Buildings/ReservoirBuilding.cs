using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class ReservoirBuilding : Building
{
    #region Properties

    /// <summary>
    /// Minimum amount of sprinkler that can be powered by the Dessalinator.
    /// </summary>
    private static int MinSprinklerPoweringCapacity = 2;

    /// <summary>
    /// Amount of water produced via Reservoir. It is calculated taking into acount the amount of water used by the Sprinkler.
    /// </summary>
    private static float WaterProductionSpeed = SprinklerBuilding.WaterQuantityToSupply * MinSprinklerPoweringCapacity + 2;

    /// <summary>
    /// Maximum amount of water that the Reservoir can hold.
    /// </summary>
    public static float MaxCapacity = SprinklerBuilding.WaterQuantityToSupply * 4;

    /// <summary>
    /// Quantity of water currently hold by the Reservoir.
    /// </summary>
    public float StoredWaterQuantity { get => _storedWaterQuantity; set { _storedWaterQuantity = Mathf.Clamp(value, 0, MaxCapacity); } }
    private float _storedWaterQuantity = 0;
    
    #endregion Properties

    #region Constructor

    public ReservoirBuilding(Vector2Int tilePosition, GameObject buildingSprite, GameObject dirtTileSprite)
        : base(BuildingType.Reservoir, tilePosition, buildingSprite, dirtTileSprite) { }

    #endregion Constructor

    #region Methods

    public override void GetWater(float waterQuantity)
    {
        Debug.Log($"Reservoir is Getting Water: {waterQuantity}");
        StoredWaterQuantity += waterQuantity;
    }

    public override bool IsFunctional(List<Tile> surroundingTiles)
    {
        return surroundingTiles.Count(tile => tile.Type == TileType.Grass) >= 4;
    }

    public override void Update(List<Tile> surroundingTiles, out TileType newType)
    {
        // Debug.Log($"Reservoir Surrounding Count: {surroundingTiles.Count}");
        newType = Type;

        bool isFunctional = IsFunctional(surroundingTiles);
        BuildingSprite.GetComponentInChildren<Animator>().SetBool("isActive", isFunctional);
        if (!isFunctional) return;

        // Debug.Log($"Reservoir Works and is Creating Water: {WaterProductionSpeed} | Stored: {StoredWaterQuantity}");
        StoredWaterQuantity += WaterProductionSpeed;
    }

    #endregion Methods
}