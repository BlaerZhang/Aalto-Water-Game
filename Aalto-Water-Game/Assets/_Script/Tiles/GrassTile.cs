using UnityEngine;
using System.Collections.Generic;

public class GrassTile : Tile
{
    private static readonly float MaxHumidityLevel = 7f;
    private float HumidityLevel = Random.Range(2, MaxHumidityLevel);
    private float DryingSpeed = Random.Range(0.2f, 1);

    public GrassTile(Vector2Int tilePosition, GameObject sprite) : base(TileType.Grass, tilePosition, sprite)
    {
        // += 1
    }

    public override TileType ApplyRulesAndGetNewType(List<Tile> surroundingTiles)
    {
        var tileTypeCounts = CountTypes(surroundingTiles);
        if (tileTypeCounts[TileType.Dirt] > 0) 
        {
            // Decrease HumidityLevel and clamp it within the valid range
            HumidityLevel = Mathf.Clamp(HumidityLevel - DryingSpeed, 0, MaxHumidityLevel);

            if (HumidityLevel == 0)
                return TileType.Dirt;
        }

        return this.Type;
    }

    public override void Destroy()
    {
        base.Destroy();
        // -= 1
    }
}
