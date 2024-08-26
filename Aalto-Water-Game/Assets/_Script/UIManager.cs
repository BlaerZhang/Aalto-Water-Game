using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    
    public List<GameObject> BuildingButtonList;

    private void OnEnable()
    {
        OnSelectedBuildingTypeChanged += UpdateUI;
    }
    
    private void OnDisable()
    {
        OnSelectedBuildingTypeChanged -= UpdateUI;
    }
    
    void Update()
    {
        // Scrolling up (forward)
        if (Input.GetAxis("Mouse ScrollWheel") <= -0.1f)
            CurrentBuildingType = (BuildingType)(((int)CurrentBuildingType + 1) % GameManager.Instance.BuildingTypeCount);
        // Scrolling down (backward)
        else if (Input.GetAxis("Mouse ScrollWheel") >= 0.1f)
            CurrentBuildingType = (BuildingType)(((int)CurrentBuildingType - 1 + GameManager.Instance.BuildingTypeCount) % GameManager.Instance.BuildingTypeCount);
    }

    public void UpdateCurrentBuildingType(int selectedBuildingType)
    {
        CurrentBuildingType = (BuildingType)selectedBuildingType;
    }

    public void UpdateUI(BuildingType selectedBuildingType)
    {
        foreach (var button in BuildingButtonList)
        {
            if (button != BuildingButtonList[(int)selectedBuildingType]) button.transform.DOScale(1, 0.1f);
            else BuildingButtonList[(int)selectedBuildingType].transform.DOScale(1.5f, 0.1f).SetEase(Ease.InElastic);
        }
    }
}
