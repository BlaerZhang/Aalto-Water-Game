using System;
using UnityEngine;
using System.Collections.Generic;

public class WaterTile: Tile
{
	public WaterTile(Vector2Int tilePosition) : base(TileType.Water, tilePosition){}

    public override TileType ApplyRulesAndGetNewType(List<Tile> surroundingTiles)
    {
        return this.Type;
    }
}
