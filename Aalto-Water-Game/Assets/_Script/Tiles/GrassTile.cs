using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GrassTile : Tile
{
    private static readonly float MaxHumidityLevel = 7f;
    private float HumidityLevel = Random.Range(2, MaxHumidityLevel);
    private float DryingSpeed = Random.Range(0.2f, 1);

    public GrassTile(Vector2Int tilePosition, GameObject sprite) : base(TileType.Grass, tilePosition, sprite) { }

    public override void Update(List<Tile> surroundingTiles, out TileType newType)
    {
        if (surroundingTiles.Any(t => t.Type == TileType.Dirt))
        {
            // Decrease HumidityLevel and clamp it within the valid range
            HumidityLevel = Mathf.Clamp(HumidityLevel - DryingSpeed, 0, MaxHumidityLevel);

            if (HumidityLevel == 0)
            {
                newType = TileType.Dirt;
                return;
            }
        }

        newType = Type;
    }
}
