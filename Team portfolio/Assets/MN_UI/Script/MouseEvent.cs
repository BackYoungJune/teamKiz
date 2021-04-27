using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void MouseEventData(PointerEventData data);

public class MouseEvent : MonoBehaviour,
    IPointerClickHandler, IPointerDownHandler,
    IDragHandler, IEndDragHandler,
    IPointerEnterHandler, IPointerExitHandler
{
    public event MouseEventData MouseClick;
    public event MouseEventData MouseDown;
    public event MouseEventData MouseDrag;
    public event MouseEventData MouseEndDrag;
    public event MouseEventData MouseEnter;
    public event MouseEventData MouseExitExit;


    public void OnPointerClick(PointerEventData eventData)
    {
        MouseClick?.Invoke(eventData);

        //Debug.Log("OnPointerClick");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        MouseDown?.Invoke(eventData);

        //Debug.Log("OnPointerDown");
    }
    public void OnDrag(PointerEventData eventData)
    {
        MouseDrag?.Invoke(eventData);

       // Debug.Log("OnDrag" + eventData.position);

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        MouseEndDrag?.Invoke(eventData);

       // Debug.Log("OnEndDrag" + eventData.position);

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseEnter?.Invoke(eventData);

       //Debug.Log("OnPointerEnter");

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        MouseExitExit?.Invoke(eventData);
       // Debug.Log("OnPointerExit");

    }
}
