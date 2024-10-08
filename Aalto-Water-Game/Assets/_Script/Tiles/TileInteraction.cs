using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileInteraction : MonoBehaviour
{
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        CursorManager.OnMouseHoverOnTile?.Invoke(transform.position, GameManager.Instance.MapManager.BuildingIsPossibleOnTile(transform.position), true);
    }

    private void OnMouseExit()
    {
        CursorManager.OnMouseHoverOnTile?.Invoke(new Vector2(100, 100), true, false);
    }

    private void OnMouseUpAsButton()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        CursorManager.OnMouseClickOnTile?.Invoke(transform.position, GameManager.Instance.MapManager.BuildingIsPossibleOnTile(transform.position));
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)) GameManager.Instance.MapManager.RemoveBuilding(transform.position);
    }
}
