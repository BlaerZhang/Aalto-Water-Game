using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public List<Sprite> BuildingSpriteList;
    public static Action<Vector2, bool, bool> OnMouseHoverOnTile;
    public static Action<Vector2, bool> OnMouseClickOnTile;

    private static AudioClip ErrorSound;

    private void OnEnable()
    {
        OnMouseHoverOnTile += UpdateCursor;
        UIManager.OnSelectedBuildingTypeChanged += ChangeCursor;
        OnMouseClickOnTile += OnCursorClickOnTile;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnDisable()
    {
        OnMouseHoverOnTile -= UpdateCursor;
        UIManager.OnSelectedBuildingTypeChanged -= ChangeCursor;
        OnMouseClickOnTile -= OnCursorClickOnTile;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateCursor(Vector2 tilePosition, bool placeable, bool display)
    {
        transform.position = tilePosition;
        _spriteRenderer.color = placeable ? new Color(0,1,0,0.7f) : new Color(1,0,0,0.7f); //set color based on placeable state
        _spriteRenderer.enabled = display;
    }

    void ChangeCursor(BuildingType buildingType)
    {
        _spriteRenderer.sprite = BuildingSpriteList[(int)buildingType];
    }

    void OnCursorClickOnTile(Vector2 tilePosition, bool isPlaceable)
    {
        if (isPlaceable) GameManager.Instance.MapManager.PlaceBuilding(tilePosition);
        else
        {
            //feedback
            //TODO play feedback sound
            GameManager.Instance.AudioManager.PlaySound(ErrorSound);
            transform.DOShakePosition(0.1f, 0.15f, 200);
        }
    }
}
