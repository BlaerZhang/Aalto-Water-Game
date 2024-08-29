using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SprinklerBuilding : Building
{
    #region Properties

    /// <summary>
    /// Amount of water that the Sprinkler supplies to its surrounding Tiles.
    /// This amount is spread evenly among the tiles in the working radius.
    /// Therefore, each tile receives: WaterQuantityToSupply/Number of Tiles affected
    /// </summary>
    public static float WaterQuantityToSupply = 24f;

    public override int SurroundingRadius { get => 2; }
    
    public static int BuildingPrice = 10;

    #endregion Properties

    #region Constructor

    public SprinklerBuilding(Vector2Int tilePosition, GameObject buildingSprite, GameObject dirtTileSprite)
        : base(BuildingType.Sprinkler, tilePosition, buildingSprite, dirtTileSprite) { }

    #endregion Constructor

    #region Methods

    public override bool IsFunctional(List<Tile> surroundingTiles)
    {
        // Debug.Log($"Surrounding Tiles Count: {surroundingTiles.Count}");
        foreach (Building building in surroundingTiles.Where(t => t.Type == TileType.Building))
        {
            // Debug.Log($"Building Type: {building.BuildingType}");
            switch (building.BuildingType)
            {
                case BuildingType.Dessalinator:
                    var dessalinator = (DessalinatorBuilding)building;
                    // Debug.Log($"Building Stored Water Quantity: {dessalinator.StoredWaterQuantity}");
                    if (dessalinator.StoredWaterQuantity >= WaterQuantityToSupply)
                    {
                        dessalinator.StoredWaterQuantity -= WaterQuantityToSupply;
                        return true;
                    }
                    break;
                case BuildingType.Reservoir:
                    var reservoir = (ReservoirBuilding)building;
                    // Debug.Log($"Building Stored Water Quantity: {reservoir.StoredWaterQuantity}");
                    if (reservoir.StoredWaterQuantity >= WaterQuantityToSupply)
                    {
                        reservoir.StoredWaterQuantity -= WaterQuantityToSupply;
                        return true;
                    }
                    break;
            }
        }

        return false;
    }

    public override void Update(List<Tile> surroundingTiles, out TileType newType)
    {
        newType = Type;
        bool isFunctional = IsFunctional(surroundingTiles);
        BuildingSprite.GetComponentInChildren<Animator>().SetBool("isActive", isFunctional);
        if (!isFunctional) return;

        foreach (var tile in surroundingTiles.Where(t => t.Type != TileType.Building))
            tile.GetWater(WaterQuantityToSupply/surroundingTiles.Count);
    }

    #endregion Methods
}