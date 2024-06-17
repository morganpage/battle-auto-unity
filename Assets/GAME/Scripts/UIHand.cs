using System;
using UnityEngine;

public class UIHand : MonoBehaviour
{
  [SerializeField] private GameObject _cardPrefab;
  [SerializeField] private Transform _cardParent;

  void OnEnable()
  {
    GameState.OnGameStateUpdated += HandleGameStateUpdated;
  }

  void OnDisable()
  {
    GameState.OnGameStateUpdated -= HandleGameStateUpdated;
  }

  private void HandleGameStateUpdated(GameState gameState)
  {
    foreach (Transform child in _cardParent)
    {
      Destroy(child.gameObject);
    }
    foreach (string characterName in gameState.Hand)
    {
      GameObject card = Instantiate(_cardPrefab, _cardParent);
      UICard uiCard = card.GetComponent<UICard>();
      Character character = GameData.GetCharacter(characterName);
      uiCard.SetFromCharacter(character);
      card.name = characterName;
    }
  }

  void Awake()
  {
    foreach (Transform child in _cardParent)
    {
      Destroy(child.gameObject);
    }
  }


}
