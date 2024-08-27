using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tooltipText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(ShowTooltip());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(ShowTooltip());
        GameManager.Instance.UIManager.HideTooltip();
    }

    IEnumerator ShowTooltip()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.UIManager.ShowTooltip(GetComponent<RectTransform>().anchoredPosition.y, tooltipText);
    }
}