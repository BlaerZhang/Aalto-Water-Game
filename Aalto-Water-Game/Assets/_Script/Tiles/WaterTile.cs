using System;
using UnityEngine;
using System.Collections.Generic;

public class WaterTile: Tile
{
	public WaterTile(Vector2Int tilePosition, GameObject sprite) : base(TileType.Water, tilePosition, sprite){}

    public override TileType ApplyRulesAndGetNewType(List<Tile> surroundingTiles)
    {
        return (TileType)UnityEngine.Random.Range(1, 3);
    }
}
