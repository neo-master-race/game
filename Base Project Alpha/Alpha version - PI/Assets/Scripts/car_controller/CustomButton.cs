using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool down;

    public void OnPointerDown(PointerEventData data)
    {
        down = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        down = false;
    }
}