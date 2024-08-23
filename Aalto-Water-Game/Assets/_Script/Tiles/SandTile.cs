using System;
using UnityEngine;
using System.Collections.Generic;

public class SandTile: Tile
{
    public SandTile(Vector2Int tilePosition) : base(TileType.Sand, tilePosition) { }

    public override TileType ApplyRulesAndGetNewType(List<Tile> surroundingTiles)
    {
        return this.Type;
    }
}
