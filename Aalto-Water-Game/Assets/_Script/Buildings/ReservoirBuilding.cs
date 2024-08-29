using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class ReservoirBuilding : Building
{
    /// <summary>
    /// Maximum amount of water that the Reservoir can hold.
    /// </summary>
    public static float MaxCapacity = 18;

    /// <summary>
    /// Speed at wich the reservoir receives water
    /// </summary>
    private static float WaterReceivingCapacity = 1f;

    /// <summary>
    /// Quantity of water currently hold by the Reservoir.
    /// </summary>
    public float StoredWaterQuantity { get => _storedWaterQuantity; set { _storedWaterQuantity = Mathf.Clamp(value, 0, MaxCapacity); } }
    private float _storedWaterQuantity = 0;

    public ReservoirBuilding(Vector2Int tilePosition, GameObject buildingSprite, GameObject dirtTileSprite)
        : base(BuildingType.Reservoir, tilePosition, buildingSprite, dirtTileSprite) { }

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
        newType = Type;
        if (!IsFunctional(surroundingTiles)) return;

        // Loop through the dissalinators with enough water to provide
        foreach (var dessalinator in surroundingTiles.OfType<DessalinatorBuilding>().Where(d => d.StoredWaterQuantity >= WaterReceivingCapacity))
        {
            GetWater(WaterReceivingCapacity);
            dessalinator.StoredWaterQuantity -= WaterReceivingCapacity;
        }
    }
}