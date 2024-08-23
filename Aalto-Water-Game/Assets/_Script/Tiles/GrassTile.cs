using System;
using UnityEngine;
using System.Collections.Generic;

public class GrassTile : Tile
{
    public GrassTile(Vector2Int tilePosition) : base(TileType.Grass, tilePosition) { }

    public override TileType ApplyRulesAndGetNewType(List<Tile> surroundingTiles)
    {
        return this.Type;
    }
}
