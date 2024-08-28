using System;
using UnityEngine;
using System.Collections.Generic;

public class RockTile: Tile
{
    public RockTile(Vector2Int tilePosition, GameObject sprite) : base(TileType.Sand, tilePosition,sprite) { }

    public override TileType ApplyRulesAndGetNewType(List<Tile> surroundingTiles)
    {
        return this.Type;
    }
}
