using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Image Mask;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    public void StartGame()
    {
        Mask.color = Color.clear;
        Mask.gameObject.SetActive(true);
        Mask.DOFade(1, 1).OnComplete(() =>
        {
            SceneManager.LoadScene("Game");
            Mask.color = Color.black;
            Mask.DOFade(0, 1).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        });
    }
}
