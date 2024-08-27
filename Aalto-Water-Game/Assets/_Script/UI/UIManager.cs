using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
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

    private float _hoverTimer = 0f;
    
    public static Action<BuildingType> OnSelectedBuildingTypeChanged;
    
    public List<GameObject> BuildingButtonList;

    public GameObject BuildingTooltip;

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

    public void ShowTooltip(float anchoredPosY, string tooltipText)
    {
        BuildingTooltip.GetComponentInChildren<TMP_Text>().text = tooltipText; // Set text
        BuildingTooltip.GetComponent<RectTransform>().DOAnchorPosY(anchoredPosY, 0.5f); // Set Y Pos
        BuildingTooltip.SetActive(true);
    }
    public void HideTooltip()
    {
        BuildingTooltip.SetActive(false);
    }
}
