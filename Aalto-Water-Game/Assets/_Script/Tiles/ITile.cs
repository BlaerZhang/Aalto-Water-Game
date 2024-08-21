using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that implements the basic behaviour of the Tiles in the Map.
/// </summary>
public interface ITile
{
    /// <summary>
    /// Type of terrain that the tile represents.
    /// </summary>
    public TileType Type { get; set; }


    /// <summary>
    /// Applies the transforming rules of the Tile according to its surrounding
    /// </summary>
    /// <param name="surroundingTiles"> List of surrounging Tiles: 
    /// <para>Top-Left; Top-Center; Top-Right; Middle-Left; Middle-Right; Bottom-Left; Bottom-Center; Bottom-Right</para>
    /// </param>
    /// <returns>Tile Type that the instance will transform into according to the rules.</returns>
    public abstract TileType ApplyRulesAndGetNewType(List<ITile?> surroundingTiles);

}

