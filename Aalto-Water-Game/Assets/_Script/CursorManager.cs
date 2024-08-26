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
        _spriteRenderer.color = placeable ? Color.green : Color.red;
        _spriteRenderer.enabled = display;
    }

    void ChangeCursor()
    {
        
    }
}
