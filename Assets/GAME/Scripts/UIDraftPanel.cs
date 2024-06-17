using System;
using TMPro;
using UnityEngine;

public class UIDraftPanel : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _textDraftPanel;

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
    _textDraftPanel.text = $"Mana: {gameState.Mana} | Turn: {gameState.Turn} | Lives: {gameState.Lives} | Victories: {gameState.Victories}";
  }

}
