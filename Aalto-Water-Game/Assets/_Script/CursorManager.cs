using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public static Action<Vector2, bool, bool> OnMouseHoverOnTile;
    // private BuildingType _currentBuildingType = BuildingType.Dessalinator;

    private void OnEnable()
    {
        OnMouseHoverOnTile += UpdateCursor;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnDisable()
    {
        OnMouseHoverOnTile -= UpdateCursor;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateCursor(Vector2 position, bool placeable, bool display)
    {
        transform.position = position;
        _spriteRenderer.color = placeable ? new Color(0,1,0,0.7f) : new Color(1,0,0,0.7f); //set color based on placeable state
        _spriteRenderer.enabled = display;
    }

    void ChangeCursor()
    {
        
    }
}