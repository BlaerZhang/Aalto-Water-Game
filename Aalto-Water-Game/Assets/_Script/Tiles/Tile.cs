using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that implements the basic behaviour of the Tiles in the Map.
/// </summary>
public abstract class Tile: Monobehavior
{
    #region Properties

    /// <summary>
    /// Type of terrain that the tile represents.
    /// </summary>
    public TileType Type { get; set; }

    public GameObject TileObject { get; }

    public Vector3 Position { get => TileObject.Position; }

    #endregion Properties

    #region Constructor

    public abstract Tile(TileType type, Vector2 tilePosition)
    {
        TileObject = Instantiate(tileType, GetTilePosition(tilePosition), Quaternion.identity);
        Type = type;
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// Create the isometric coordinates of the tile on the Map.
    /// </summary>
    /// <param name="tilePosition">d(x, y) coordinates of the tile in a cartesian plane.</param>
    /// <returns>3D Vector representing the tile's position on the Map.</returns>
    private static Vector3 GetTilePosition(Vector2Int tilePosition)
    {
        float tileSize = tilePrefabList[Random.Range(0, 2)].transform.localScale.x;
        float halfTileSize = tileSize / 2f;
        float x = (tilePosition.x - tilePosition.y) * halfTileSize;
        float y = (tilePosition.x + tilePosition.y) * halfTileSize * 0.5f;

        return new Vector3(x, y, 0f);
    }

    /// <summary>
    /// Applies the transforming rules of the Tile according to its surrounding
    /// </summary>
    /// <param name="surroundingTiles"> List of surrounging Tiles: 
    /// <para>Top-Left; Top-Center; Top-Right; Middle-Left; Middle-Right; Bottom-Left; Bottom-Center; Bottom-Right</para>
    /// </param>
    /// <returns>Tile Type that the instance will transform into according to the rules.</returns>
    public abstract TileType ApplyRulesAndGetNewType(List<Tile?> surroundingTiles);

    public Tile CreateTile(TileType type, Vector2Int position)
    {
        switch (type)
        {
            case TileType.Water:
                return new WaterTile(position);
            case TileType.Grass:
                return new GrassTile(position);
            case TileType.Dirt:
                return new DirtTile(position);
            case TileType.Sand:
                return new SandTile(position);
            case TileType.SaltyWater:
                return new SaltyWaterTile(position);
            // Add cases for other tile types
            default:
                throw new ArgumentException("Unknown tile type");
        }
    }

    #endregion Methods
}

