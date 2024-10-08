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

    /// <summary>
    /// This flag defines if the left click defines an action of Creating a Building (true) or Removing a Building (false)
    /// </summary>
    public static bool IsCreateBuildingMode = true;

    public List<GameObject> BuildingButtonList;

    public GameObject BuildingTooltip;

    public Slider ProgressBar;

    public TMP_Text Resource;

    public TMP_Text Objective;

    public TMP_Text LevelHint;

    public TMP_Text LevelName;

    public TMP_Text Score;

    public Image Mask;

    public Image BuildingModeMask;

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

        // if (Input.GetKeyDown(KeyCode.R)) LoadNextLevel(true);
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
        ProgressBar.DOValue(percentage, 0.5f).SetEase(Ease.InOutQuad);
    }

    public void UpdateResource(int resource)
    {
        Resource.DOText($"{resource}", 0.25f, true, ScrambleMode.Numerals);
    }

    public void UpdateObjective(int targetTileNumber, TileType targetTileType, BuildingType targetBuildingType = BuildingType.Dessalinator)
    {
        string color = "#1362c8";
        if (targetTileType == TileType.Building) Objective.text = $"Build at least <color={color}>{targetTileNumber}</color> functional <color={color}>{targetBuildingType}</color>";
        else Objective.text = $"Convert at least <color={color}>{targetTileNumber}</color> tiles into <color={color}>{targetTileType}</color>";
    }

    public void UpdateLevelHint(string hint)
    {
        LevelHint.text = hint;
    }

    public void UpdateLevelText(int levelIndex)
    {
        LevelName.text = $"Level {levelIndex}";
    }

    public void UpdateScore(int score)
    {
        Score.DOText($"Your Score: {score}", 0.25f, true, ScrambleMode.All);
    }

    public void ShowTooltip(float anchoredPosY, string tooltipText)
    {
        BuildingTooltip.GetComponentInChildren<TMP_Text>().text = tooltipText; // Set text
        BuildingTooltip.GetComponent<RectTransform>().DOAnchorPosY(anchoredPosY, 0f); // Set Y Pos
        BuildingTooltip.transform.DOScale(1, 0.25f).SetEase(Ease.OutElastic, 1, 0.1f);
    }
    public void HideTooltip()
    {
        BuildingTooltip.transform.DOScale(0, 0.1f);
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

    public void SwitchBuildMode()
    {
        IsCreateBuildingMode = !IsCreateBuildingMode;
        BuildingModeMask.gameObject.SetActive(!IsCreateBuildingMode);
    }

    public void LoadNextLevel(bool reload = false)
    {
        Mask.color = Color.clear;
        Mask.gameObject.SetActive(true);
        Mask.DOFade(1, 1).OnComplete(() =>
        {
            if(!reload) GameManager.Instance.LevelManager.LoadLevel(GameManager.Instance.LevelManager.CurrentLevelIndex + 1, true);
            else GameManager.Instance.LevelManager.LoadLevel(GameManager.Instance.LevelManager.CurrentLevelIndex, true);
            HideEndScreen();
            Mask.color = Color.black;
            Mask.DOFade(0, 1).OnComplete(() =>
            {
                Mask.gameObject.SetActive(false);
            });
        });
    }
}
