using System;
using UnityEngine;
using System.Collections.Generic;

public class RockTile: Tile
{
<<<<<<< HEAD
    public RockTile(Vector2Int tilePosition, GameObject sprite) : base(TileType.Rock, tilePosition,sprite) { }
=======
    public RockTile(Vector2Int tilePosition, GameObject sprite) : base(TileType.Sand, tilePosition,sprite) { }
>>>>>>> origin/main

    public override TileType ApplyRulesAndGetNewType(List<Tile> surroundingTiles)
    {
        return this.Type;
    }
}
