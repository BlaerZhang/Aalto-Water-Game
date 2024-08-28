using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [HideInInspector] public int CurrentLevelIndex = 0;
    [HideInInspector] public int TargetTileNumber;
    public int CurrentTileNumber 
    { 
        get => _currentTileNumber;
        set
        {
            _currentTileNumber = value;
            GameManager.Instance.UIManager.UpdateProgressBar((float)_currentTileNumber / TargetTileNumber);
            if (_currentTileNumber >= TargetTileNumber) StartCoroutine(CheckWinning());
            else StopAllCoroutines();
        }
    }
    private int _currentTileNumber;
    public List<LevelInfoSO> LevelInfoSOList;
    [HideInInspector] public LevelInfoSO CurrentLevelInfoSOList;

    private void Start()
    {
        LoadLevel(0, true);
    }

    public void LoadLevel(int levelIndex, bool skipLoadingScene = false)
    {
        if (!skipLoadingScene) SceneManager.LoadScene($"Level {levelIndex + 1}");
        CurrentLevelIndex = levelIndex;
        CurrentLevelInfoSOList = LevelInfoSOList[CurrentLevelIndex];
        TargetTileNumber = CurrentLevelInfoSOList.RequiredNumber;
        CurrentTileNumber = 0;

        // Debug.Log($"Current Level: {CurrentLevelIndex} \n Target Tile Number: {TargetTileNumber}");
        
        GameManager.Instance.MapManager = FindObjectOfType<MapManager>();
        Dictionary<Vector2Int, TileType> mapAsDictionary = CSVReader.ReadCSV(levelIndex, out int mapWidth, out int mapHeight);
        GameManager.Instance.MapManager.Map = new Dictionary<Vector2Int, Tile>();
        GameManager.Instance.MapManager.MapHeight = 50;
        GameManager.Instance.MapManager.MapWidth = 50;
        GameManager.Instance.MapManager.GenerateMap();
        GameManager.Instance.MapManager.GenerateMapFromDictionary(mapAsDictionary, mapHeight, mapWidth);
        // Debug.Log(GameManager.Instance.MapManager.Map.Count);
    }
    
    IEnumerator CheckWinning()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.UIManager.ShowEndScreen(true);
    }
}
