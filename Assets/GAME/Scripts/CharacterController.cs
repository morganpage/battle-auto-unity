using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
  public GameObject currentCharacter;
  public int currentSlot;
  public int startSlot;
  [SerializeField] private Image[] _slots;
  [SerializeField] private Color _highlightedColor = Color.green;
  private Color _defaultColor;



  void Awake()
  {
    _defaultColor = _slots[0].color;
  }

  void OnEnable()
  {
    CharacterDrag.OnSelected += HandleCharacterSelected;
    CharacterDrag.OnEndDrag += HandleEndDrag;
  }

  private void HandleCharacterSelected(GameObject card)
  {
    currentCharacter = card;
    startSlot = -1;
  }

  void OnDisable()
  {
    CharacterDrag.OnSelected -= HandleCharacterSelected;
    CharacterDrag.OnEndDrag -= HandleEndDrag;
  }

  private void HandleEndDrag(GameObject card)
  {
    currentCharacter = null;
    int fieldIndexStart = startSlot;
    int fieldIndexEnd = currentSlot;
    GameManager.MoveCharacter(fieldIndexStart, fieldIndexEnd);
    for (int i = 0; i < _slots.Length; i++)
    {
      _slots[i].color = _defaultColor;
    }
  }

  void Update()
  {
    if (!currentCharacter) return;
    for (int i = 0; i < _slots.Length; i++)
    {
      _slots[i].color = _defaultColor;
    }
    currentSlot = -1;
    Rect rectCurrentCard = GetRectFromRectTransform(currentCharacter.GetComponent<RectTransform>());
    for (int i = 0; i < _slots.Length; i++)
    {
      Rect slotRect = GetRectFromRectTransform(_slots[i].GetComponent<RectTransform>());
      if (slotRect.Overlaps(rectCurrentCard))
      {
        _slots[i].color = _highlightedColor;
        currentSlot = i;
        if (startSlot == -1) startSlot = i;
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
