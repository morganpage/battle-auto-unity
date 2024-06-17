using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrag : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
  public static Action<GameObject> OnSelected;
  public static Action<GameObject> OnEndDrag;
  private Vector2 _offset;
  private Vector3 _defaultPosition;
  public void OnDrag(PointerEventData eventData)
  {
    transform.position = eventData.position + _offset;
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    _offset = (Vector2)transform.position - eventData.position;
    _defaultPosition = transform.position;
    OnSelected?.Invoke(this.gameObject);
  }

  public void OnPointerUp(PointerEventData eventData)
  {
    transform.position = _defaultPosition;
    OnEndDrag?.Invoke(this.gameObject);
  }
}
