using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WaterTile: Tile
{

    #region Properties

    private static readonly float MaxHumidityLevel = 7f;
    private float HumidityLevel = UnityEngine.Random.Range(2, MaxHumidityLevel);
    private float DryingSpeed = UnityEngine.Random.Range(0.2f, 1);

    #endregion Properties

    public WaterTile(Vector2Int tilePosition, GameObject sprite) : base(TileType.Water, tilePosition, sprite){}

    public override void Update(List<Tile> surroundingTiles, out TileType newType)
    {
        newType = Type;
        if (surroundingTiles.Any(t => t.Type == TileType.Dirt))
        {
            // Decrease HumidityLevel and clamp it within the valid range
            HumidityLevel = Mathf.Clamp(HumidityLevel - DryingSpeed, 0, MaxHumidityLevel);

            if (HumidityLevel == 0)
                newType = TileType.Dirt;
        }


    }
}
