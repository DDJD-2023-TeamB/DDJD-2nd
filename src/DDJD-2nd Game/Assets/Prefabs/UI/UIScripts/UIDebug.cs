using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDebug : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    private void Start()
    {
        Debug.Log("Starting UI Debug Script");
    }

    private void OnMouseOver()
    {
        Debug.Log("Mouse over ui collider!");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("UI element was clicked!");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entered gameObject");
    }
}
