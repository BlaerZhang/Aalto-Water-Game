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
        // Scrolling up (forward)
        if (Input.GetAxis("Mouse ScrollWheel") > 0.1f)
            CurrentBuildingType = (BuildingType)(((int)CurrentBuildingType + 1) % GameManager.Instance.BuildingTypeCount);
        // Scrolling down (backward)
        else if (Input.GetAxis("Mouse ScrollWheel") < 0.1f)
            CurrentBuildingType = (BuildingType)(((int)CurrentBuildingType - 1 + GameManager.Instance.BuildingTypeCount) % GameManager.Instance.BuildingTypeCount);
    }

    public void UpdateCurrentBuildingType(int selectedBuildingType)
    {
        CurrentBuildingType = (BuildingType)selectedBuildingType;
    }
}
