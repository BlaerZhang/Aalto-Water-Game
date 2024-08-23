using System;

public class SaltyWaterTile: Tile
{
	public SaltyWaterTile(Vector2Int tilePosition) : base(TileType.SaltyWater, tilePosition){}

    public override TileType ApplyRulesAndGetNewType(List<ITile?> surroundingTiles)
	{
		return this.Type;
	}
}
