using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class UICharacter : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _textName;
  [SerializeField] private TextMeshProUGUI _textDescription;
  [SerializeField] private TextMeshProUGUI _textTier;
  [SerializeField] private TextMeshProUGUI _textAttack;
  [SerializeField] private TextMeshProUGUI _textHealth;
  [SerializeField] private Image _characterImage;

  public void SetFromCharacter(Character character)
  {
    _textName.text = character.name;
    _textDescription.text = character.description;
    _textTier.text = character.tier.ToString();
    _textAttack.text = character.attack.ToString();
    _textHealth.text = character.health.ToString();
    SpriteLibraryAsset spriteLibrary = Resources.Load<SpriteLibraryAsset>("Sprite Library");
    _characterImage.sprite = spriteLibrary.GetSprite("characters", character.name);
  }
}
