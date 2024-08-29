using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class LevelInfoSO : ScriptableObject
{
    [Header("Objective")]
    public TileType RequiredTileType;
    public BuildingType RequiredBuildingTypeIfRequiringBuilding;
    public int RequiredTileNumber;
    
    [Header("Limit")]
    public int ResourcesAvailable;
    
    [Header("Info")]
    public string LevelHint;
}
