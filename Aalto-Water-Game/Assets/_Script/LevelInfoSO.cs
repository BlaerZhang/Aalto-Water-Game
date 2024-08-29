using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class LevelInfoSO : ScriptableObject
{
    public TileType RequiredTileType;
    public BuildingType RequiredBuildingTypeIfRequiringBuilding;
    public int RequiredTileNumber;
    public int ResourcesAvailable;
    public string LevelHint;
}
