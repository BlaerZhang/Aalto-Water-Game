using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    [HideInInspector] public int CurrentLevelIndex = 0;
    [HideInInspector] public int TargetTileNumber;
    // [HideInInspector] public int ResourceLimit;

    public int CurrentResource
    {
        get => _currentResource;
        set
        {
            _currentResource = (int)Mathf.Clamp(value, 0, Single.PositiveInfinity);
            GameManager.Instance.UIManager.UpdateResource(_currentResource);
            if (_currentResource < 10f) Invoke("CheckLosing", 5);
            else CancelInvoke();
        }
    }
    private int _currentResource;
    
    public int CurrentTileNumber 
    { 
        get => _currentTileNumber;
        set
        {
            _currentTileNumber = (int)Mathf.Clamp(value, 0, Single.PositiveInfinity);
            GameManager.Instance.UIManager.UpdateProgressBar((float)_currentTileNumber / TargetTileNumber);
            if (_currentTileNumber >= TargetTileNumber) StartCoroutine(CheckWinning());
            else StopAllCoroutines();
        }
    }
    private int _currentTileNumber;
    public List<LevelInfoSO> LevelInfoSOList;
    [FormerlySerializedAs("CurrentLevelInfoSOList")] [HideInInspector] public LevelInfoSO CurrentLevelInfoSO;

    private void Start()
    {
        LoadLevel(0, true);
    }

    public void LoadLevel(int levelIndex, bool skipLoadingScene = false)
    {
        if (!skipLoadingScene) SceneManager.LoadScene($"Level {levelIndex + 1}"); //Load Scene (Legacy)
        
        CurrentLevelIndex = levelIndex;
        CurrentLevelInfoSO = LevelInfoSOList[CurrentLevelIndex];
        TargetTileNumber = CurrentLevelInfoSO.RequiredTileNumber; //Set target number
        CurrentTileNumber = 0; //Init current number
        CurrentResource = CurrentLevelInfoSO.ResourcesAvailable; //Init resource
        GameManager.Instance.UIManager.UpdateObjective(
            TargetTileNumber, 
            CurrentLevelInfoSO.RequiredTileType,
            CurrentLevelInfoSO.RequiredBuildingTypeIfRequiringBuilding); //Update Objective UI
        GameManager.Instance.UIManager.UpdateLevelHint(CurrentLevelInfoSO.LevelHint); //Update level hint
        GameManager.Instance.UIManager.UpdateLevelText(CurrentLevelIndex + 1); //Update level text
        
        //Generate Map
        GameManager.Instance.MapManager = FindObjectOfType<MapManager>();
        Dictionary<Vector2Int, TileType> mapAsDictionary = CSVReader.ReadCSV(levelIndex, out int mapWidth, out int mapHeight);
        GameManager.Instance.MapManager.Map = new Dictionary<Vector2Int, Tile>();
        GameManager.Instance.MapManager.MapHeight = 50;
        GameManager.Instance.MapManager.MapWidth = 50;
        GameManager.Instance.MapManager.GenerateMap();
        GameManager.Instance.MapManager.GenerateMapFromDictionary(mapAsDictionary, mapHeight, mapWidth);
    }
    
    IEnumerator CheckWinning()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.UIManager.ShowEndScreen(true);
        GameManager.Instance.UIManager.UpdateScore(_currentResource);
    }

    void CheckLosing()
    {
        GameManager.Instance.UIManager.ShowEndScreen(false);
    }
}
