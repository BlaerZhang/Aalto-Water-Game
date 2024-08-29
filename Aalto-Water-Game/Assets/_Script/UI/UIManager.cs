using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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

    public GameObject BuildingTooltip;

    public Slider ProgressBar;

    public Image Mask;

    [Header("End Screen")]
    public GameObject levelCompleteScreen;
    public GameObject levelFailedScreen;

    private void OnEnable()
    {
        OnSelectedBuildingTypeChanged += UpdateButtonUI;
    }
    
    private void OnDisable()
    {
        OnSelectedBuildingTypeChanged -= UpdateButtonUI;
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

    public void UpdateButtonUI(BuildingType selectedBuildingType)
    {
        foreach (var button in BuildingButtonList)
        {
            if (button != BuildingButtonList[(int)selectedBuildingType]) button.transform.DOScale(1, 0.1f);
            else BuildingButtonList[(int)selectedBuildingType].transform.DOScale(1.5f, 0.1f).SetEase(Ease.InElastic);
        }
    }

    public void UpdateProgressBar(float percentage)
    {
        ProgressBar.DOValue(percentage, 0.25f);
    }

    public void UpdateResource(int resource)
    {
        
    }

    public void ShowTooltip(float anchoredPosY, string tooltipText)
    {
        BuildingTooltip.GetComponentInChildren<TMP_Text>().text = tooltipText; // Set text
        BuildingTooltip.GetComponent<RectTransform>().DOAnchorPosY(anchoredPosY, 0f); // Set Y Pos
        BuildingTooltip.SetActive(true);
    }
    public void HideTooltip()
    {
        BuildingTooltip.SetActive(false);
    }

    public void ShowEndScreen(bool winning)
    {
        if (winning) levelCompleteScreen.SetActive(true);
        else levelFailedScreen.SetActive(true);
    }

    public void HideEndScreen()
    {
        levelCompleteScreen.SetActive(false);
        levelFailedScreen.SetActive(false);
    }
    
    public void LoadNextLevel()
    {
        Mask.color = Color.clear;
        Mask.gameObject.SetActive(true);
        Mask.DOFade(1, 1).OnComplete(() =>
        {
            GameManager.Instance.LevelManager.LoadLevel(GameManager.Instance.LevelManager.CurrentLevelIndex + 1, true);
            HideEndScreen();
            Mask.color = Color.black;
            Mask.DOFade(0, 1).OnComplete(() =>
            {
                Mask.gameObject.SetActive(false);
            });
        });
    }
}
