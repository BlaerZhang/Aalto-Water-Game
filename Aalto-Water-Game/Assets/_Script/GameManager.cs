using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector] public MapManager MapManager;

    [HideInInspector] public UIManager UIManager;

    [HideInInspector] public LevelManager LevelManager;

    [HideInInspector] public AudioManager AudioManager;

    public int BuildingTypeCount = Enum.GetValues(typeof(BuildingType)).Length;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        UIManager = GetComponentInChildren<UIManager>();
        LevelManager = GetComponentInChildren<LevelManager>();
        AudioManager = GetComponentInChildren<AudioManager>();
    }
}
