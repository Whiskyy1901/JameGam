using UnityEngine;
using UnityEngine.EventSystems;


public class GuyClickable : MonoBehaviour, IPointerDownHandler
{
    public ClickManager clicker;
    public void OnPointerDown(PointerEventData e) => clicker.OnClick();
}