using System;

public class GrassTile : Tile
{
    public GrassTile(Vector2Int tilePosition) : base(TileType.Plant, tilePosition) { }

    public override TileType ApplyRulesAndGetNewType(List<ITile?> surroundingTiles)
    {
        return this.Type;
    }
}
