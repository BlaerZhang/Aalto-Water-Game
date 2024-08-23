using System;

public class DirtTile : Tile
{
    public DirtTile(Vector2Int tilePosition) : base(TileType.Dirt, tilePosition) { }

    public override TileType ApplyRulesAndGetNewType(List<ITile?> surroundingTiles)
    {
        return this.Type;
    }
}
