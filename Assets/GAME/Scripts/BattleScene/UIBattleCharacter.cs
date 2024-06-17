using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class UIBattleCharacter : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _textUID;
  [SerializeField] private TextMeshProUGUI _textTier;
  [SerializeField] private TextMeshProUGUI _textAttack;
  [SerializeField] private TextMeshProUGUI _textHealth;
  [SerializeField] private Image _characterImage;

  public string UID { get { return _textUID.text; } }

  public void SetFromCharacter(BattleCharacter battleCharacter)
  {
    _textUID.text = battleCharacter.UID;
    _textTier.text = battleCharacter.tier.ToString();
    _textAttack.text = battleCharacter.attack.ToString();
    _textHealth.text = battleCharacter.health.ToString();
    SpriteLibraryAsset spriteLibrary = Resources.Load<SpriteLibraryAsset>("Sprite Library");
    _characterImage.sprite = spriteLibrary.GetSprite("characters", battleCharacter.name);
  }

  public bool Flip
  {
    set
    {
      _characterImage.GetComponent<RectTransform>().localScale = new Vector3((value ? -1 : 1), 1, 1);
    }
  }

}
