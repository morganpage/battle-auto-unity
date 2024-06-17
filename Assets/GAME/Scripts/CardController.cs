using System;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
  public GameObject currentCard;
  public int currentSlot;
  [SerializeField] private Image[] _slots;
  [SerializeField] private Color _highlightedColor = Color.green;
  private Color _defaultColor;

  public Vector3 GetSlotPosition(int index)
  {
    return _slots[index].transform.position;
  }

  void Awake()
  {
    _defaultColor = _slots[0].color;
  }

  void OnEnable()
  {
    CardDrag.OnSelected += HandleCardSelected;
    CardDrag.OnEndDrag += HandleEndDrag;
  }

  private void HandleCardSelected(GameObject card)
  {
    currentCard = card;
  }

  void OnDisable()
  {
    CardDrag.OnSelected -= HandleCardSelected;
    CardDrag.OnEndDrag -= HandleEndDrag;
  }

  private void HandleEndDrag(GameObject card)
  {
    currentCard = null;
    int handIndex = card.transform.GetSiblingIndex();
    int fieldIndex = currentSlot;
    GameManager.PlayFromHand(handIndex, fieldIndex);
    for (int i = 0; i < _slots.Length; i++)
    {
      _slots[i].color = _defaultColor;
    }
  }

  void Update()
  {
    if (!currentCard) return;
    for (int i = 0; i < _slots.Length; i++)
    {
      _slots[i].color = _defaultColor;
    }
    currentSlot = -1;
    Rect rectCurrentCard = GetRectFromRectTransform(currentCard.GetComponent<RectTransform>());
    for (int i = 0; i < _slots.Length; i++)
    {
      Rect slotRect = GetRectFromRectTransform(_slots[i].GetComponent<RectTransform>());
      if (slotRect.Overlaps(rectCurrentCard))
      {
        _slots[i].color = _highlightedColor;
        currentSlot = i;
        return;
      }
    }

  }

  Rect GetRectFromRectTransform(RectTransform rectTransform)
  {
    Vector3[] corners = new Vector3[4];
    rectTransform.GetWorldCorners(corners);
    return new Rect(corners[0].x, corners[0].y, corners[2].x - corners[0].x, corners[2].y - corners[0].y);
  }
}
