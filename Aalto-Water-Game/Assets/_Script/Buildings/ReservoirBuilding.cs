using UnityEngine;

internal class ReservoirBuilding : Building
{
    /// <summary>
    /// Maximum amount of water that the Reservoir can hold.
    /// </summary>
    public static float MaxCapacity = 18;

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
}