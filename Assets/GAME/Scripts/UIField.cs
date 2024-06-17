using System;
using System.Collections;
using UnityEngine;

public class UIField : MonoBehaviour
{
  [SerializeField] private GameObject _characterPrefab;
  [SerializeField] private Transform _characterParent;
  [SerializeField] private CardController _cardController;

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
    StartCoroutine(HandleGameStateUpdatedCo(gameState));
  }

  IEnumerator HandleGameStateUpdatedCo(GameState gameState)
  {
    yield return new WaitForEndOfFrame();
    foreach (Transform child in _characterParent)
    {
      Destroy(child.gameObject);
    }

    foreach (var characterKVP in gameState.Field)
    {
      int fieldIndex = characterKVP.Key;
      Character character = characterKVP.Value;
      GameObject characterGO = Instantiate(_characterPrefab, _characterParent);
      UICharacter uiCharacter = characterGO.GetComponent<UICharacter>();
      uiCharacter.SetFromCharacter(character);
      characterGO.name = character.name;
      characterGO.transform.position = _cardController.GetSlotPosition(fieldIndex);
    }
  }
}
