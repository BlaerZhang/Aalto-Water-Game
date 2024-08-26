using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GrassTile : Tile
{
    public GrassTile(Vector2Int tilePosition, GameObject sprite) : base(TileType.Grass, tilePosition,sprite) { }

    public override TileType ApplyRulesAndGetNewType(List<Tile> surroundingTiles)
    {
        return (TileType)Random.Range(1, 3);
    }
}
