using System;
using UnityEngine;
using System.Collections.Generic;

public class GrassTile : Tile
{
    public GrassTile(Vector2Int tilePosition, GameObject sprite) : base(TileType.Grass, tilePosition,sprite) { }

    public override TileType ApplyRulesAndGetNewType(List<Tile> surroundingTiles)
    {
        return this.Type;
    }
}
