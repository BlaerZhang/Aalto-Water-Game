using System;

public class WaterTile: Tile
{
	public WaterTile(Vector2Int tilePosition) : base(TileType.Water, tilePosition){}

    public override TileType ApplyRulesAndGetNewType(List<ITile?> surroundingTiles)
    {
        return this.Type;
    }
}
