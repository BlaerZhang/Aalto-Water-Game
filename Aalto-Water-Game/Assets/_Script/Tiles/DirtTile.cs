using System;
using System.Collections.Generic;
using UnityEngine;

public class DirtTile : Tile
{
    public DirtTile(Vector2Int tilePosition, GameObject sprite) : base(TileType.Dirt, tilePosition,sprite) { }

    public override TileType ApplyRulesAndGetNewType(List<Tile> surroundingTiles)
    {
        throw new NotImplementedException();
    }
}
