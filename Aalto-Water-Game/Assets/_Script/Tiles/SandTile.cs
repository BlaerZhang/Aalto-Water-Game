using System;

public class SandTile: Tile
{
    public SandTile(Vector2Int tilePosition) : base(TileType.Sand, tilePosition) { }

    public override TileType ApplyRulesAndGetNewType(List<ITile?> surroundingTiles)
    {
        return this.Type;
    }
}
