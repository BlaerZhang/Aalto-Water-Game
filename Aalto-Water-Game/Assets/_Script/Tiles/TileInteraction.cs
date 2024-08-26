using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInteraction : MonoBehaviour
{
    private void OnMouseEnter()
    {
        CursorManager.OnMouseHoverOnTile(transform.position, true, true);
    }

    private void OnMouseExit()
    {
        CursorManager.OnMouseHoverOnTile(new Vector2(100, 100), true, false);
    }

    private void OnMouseDown()
    {
        print($"click {transform.position}");
    }
}
