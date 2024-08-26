using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public BuildingType CurrentBuildingType
    {
        get => _buildingType;
        set
        {
            _buildingType = value;
            OnSelectedBuildingTypeChanged?.Invoke(value);
        }
    }
    private BuildingType _buildingType;
    
    public static Action<BuildingType> OnSelectedBuildingTypeChanged;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCurrentBuildingType(int selectedBuildingType)
    {
        CurrentBuildingType = (BuildingType)selectedBuildingType;
    }
}
