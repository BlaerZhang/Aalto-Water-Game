using System;
using UnityEngine;
using System.Collections.Generic;

public class RockTile: Tile
{
    public RockTile(Vector2Int tilePosition, GameObject sprite) : base(TileType.Rock, tilePosition,sprite) { }

    public override void Update(List<Tile> surroundingTiles, out TileType newType)
    {
        newType = Type;
    }
}
