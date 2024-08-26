using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class that implements the basic behaviour of the Tiles in the Map.
/// </summary>
public abstract class Tile
{
    #region Properties

    /// <summary>
    /// Type of terrain that the tile represents.
    /// </summary>
    public TileType Type { get; set; }

    public GameObject Sprite { get; }

    public Vector3 Position { get => Sprite.transform.position; }

    #endregion Properties

    #region Constructor

    public Tile(TileType type, Vector2Int tilePosition, GameObject sprite)
    {
        Sprite = sprite;
        Type = type;
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// Create the isometric coordinates of the tile on the Map.
    /// </summary>
    /// <param name="tilePosition">d(x, y) coordinates of the tile in a cartesian plane.</param>
    /// <returns>3D Vector representing the tile's position on the Map.</returns>
    public static Vector3 GetTilePosition(Vector2Int tilePosition)
    {
        float tileSize = 1f;
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
    public abstract TileType ApplyRulesAndGetNewType(List<Tile> surroundingTiles);

    public static TileType GetTileBasedOnSurrounding(List<Tile> surroundingTiles)
    {
        var typeCounts = surroundingTiles
        .Where(tile => tile != null) // Filter out null tiles
        .GroupBy(tile => tile.Type)  // Group by TileType
        .ToDictionary(group => group.Key, group => group.Count());

        // Check if the surrounding tiles contain enough Water tiles
        if (typeCounts.ContainsKey(TileType.Water) &&  typeCounts[TileType.Water] > 3 && UnityEngine.Random.value > 0.75f)
        {
            return TileType.Water; // Make this tile water if surrounded by mostly water and random condition met
        }

        // Check if the surrounding tiles contain enough Dirt tiles
        if (typeCounts.ContainsKey(TileType.Dirt) && typeCounts[TileType.Dirt] > 2 && UnityEngine.Random.value > 0.55f)
        {
            return TileType.Water; // Make this tile water if surrounded by mostly dirt and random condition met
        }

        // Check if the surrounding tiles contain enough Dirt tiles
        if (typeCounts.ContainsKey(TileType.Grass) && typeCounts[TileType.Grass] <= 3 && UnityEngine.Random.value > 0.30f)
        {
            return TileType.Grass; // Make this tile water if surrounded by mostly dirt and random condition met
        }

        // Default case
        return TileType.Dirt;
    }

    public static Tile CreateTile(TileType type, Vector2Int position, GameObject sprite)
    {
        switch (type)
        {
            case TileType.Water:
                return new WaterTile(position, sprite);
            case TileType.Grass:
                return new GrassTile(position, sprite);
            case TileType.Dirt:
                return new DirtTile(position, sprite);
            case TileType.Sand:
                return new SandTile(position, sprite);
            case TileType.SaltyWater:
                return new SaltyWaterTile(position, sprite);
            // Add cases for other tile types
            default:
                Debug.Log($"Tile type {type.ToString()} does not exist");
                return null;
        }
    }

    #endregion Methods
}

