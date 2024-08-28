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
        CurrentLevelIndex = -1;
        // CurrentLevelInfoSOList = LevelInfoSOList[CurrentLevelIndex];
        // TargetTileNumber = LevelInfoSOList[0].RequiredNumber;
        CurrentTileNumber = 0;
    }

    public void LoadLevel(int levelIndex)
    {
        CurrentLevelIndex = levelIndex;
        CurrentLevelInfoSOList = LevelInfoSOList[CurrentLevelIndex];
        SceneManager.LoadScene($"Level {levelIndex + 1}");
        TargetTileNumber = LevelInfoSOList[levelIndex].RequiredNumber;
        CurrentTileNumber = 0;
    }

    IEnumerator CheckWinning()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.UIManager.ShowEndScreen(true);
    }
}
