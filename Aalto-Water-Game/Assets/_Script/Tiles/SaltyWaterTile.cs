using System;
using UnityEngine;
using System.Collections.Generic;

public class SaltyWaterTile: Tile
{
	public SaltyWaterTile(Vector2Int tilePosition, GameObject sprite) : base(TileType.SaltyWater, tilePosition,sprite){}

    public override TileType ApplyRulesAndGetNewType(List<Tile> surroundingTiles)
	{
		return this.Type;
	}
}
