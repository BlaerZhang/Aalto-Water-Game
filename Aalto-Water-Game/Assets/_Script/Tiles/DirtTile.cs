using System;
using System.Collections.Generic;
using UnityEngine;

public class DirtTile : Tile
{
    #region Properties

    /// <summary>
    /// Amount of Water contained in the Tile.
    /// </summary>
    private float HumidityLevel = 0;

    /// <summary>
    /// Maximum amount of water that the Tile can contain
    /// </summary>
    private static readonly float MaxHumidity = 7f;

    /// <summary>
    /// The Tile becomes Fertile once the Humidity Level reaches this value.
    /// </summary>
    private static readonly float FertilityThreshold = 0.01f;

    /// <summary>
    /// Base probability for a Dirt Tile to become a Grass Tile once the Fertility Threshold is reached by the Humidity Level.
    /// </summary>
    private static readonly float MinGrassProbability = 1f;

    #endregion Properties

    public DirtTile(Vector2Int tilePosition, GameObject sprite) : base(TileType.Dirt, tilePosition,sprite) { }

    public override void Update(List<Tile> surroundingTiles, out TileType newType)
    {
        newType = Type;

        float probabilityThreshold = Mathf.Clamp01(MinGrassProbability + (HumidityLevel - FertilityThreshold) * 0.05f);
        if (HumidityLevel > FertilityThreshold && UnityEngine.Random.value <= probabilityThreshold)
            newType = TileType.Grass;
    }

    public override void GetWater(float waterQuantity)
    {
        HumidityLevel = Mathf.Clamp(HumidityLevel + waterQuantity, 0, MaxHumidity);
    }
}
