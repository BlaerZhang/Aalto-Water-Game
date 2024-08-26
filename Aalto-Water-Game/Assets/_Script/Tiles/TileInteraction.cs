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
        CursorManager.OnMouseHoverOnTile(transform.position, MapManager.Instance.BuildingIsPossibleOnTile(transform.position), true);
    }

    private void OnMouseExit()
    {
        CursorManager.OnMouseHoverOnTile(new Vector2(100, 100), true, false);
    }

    private void OnMouseUpAsButton()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        MapManager.Instance.PlaceBuilding(transform.position);
    }
}
