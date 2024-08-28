using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

[CreateAssetMenu]
public class LevelInfoSO : ScriptableObject
{
    public TileType RequiredTileType;
    public BuildingType RequiredBuildingTypeIfRequiringBuilding;
    public int RequiredNumber;
}
