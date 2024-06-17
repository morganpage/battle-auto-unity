using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBattlePanel : MonoBehaviour
{
  [SerializeField] private GameObject _battleOverPanel;
  [SerializeField] private TextMeshProUGUI _battleOverText; //ie Battle Lost!!! Battle Won!!!!
  [SerializeField] private TextMeshProUGUI _battleOverStats; //ie Lives: 2 | Victories: 3
  GameState _gameState;
  void OnEnable()
  {
    BattleState.OnBattleRender += HandleBattleRender;
    GameState.OnGameStateUpdated += HandleGameStateUpdated;
  }

  void OnDisable()
  {
    BattleState.OnBattleRender -= HandleBattleRender;
    GameState.OnGameStateUpdated -= HandleGameStateUpdated;
  }
  private void HandleBattleRender(BattleState battleState)
  {
    _battleOverPanel.SetActive(false);
    if (battleState.Action == "BattleDraw")
    {
      _battleOverText.text = "Draw!";
      _battleOverPanel.SetActive(true);
    }
    if (battleState.Action == "BattleLost")
    {
      _battleOverText.text = _gameState.Lives <= 0 ? "Game Over!" : "Battle Lost!!!";
      _battleOverPanel.SetActive(true);
    }
    if (battleState.Action == "BattleWon")
    {
      _battleOverText.text = _gameState.Victories >= 3 ? "Game Won!" : "Battle Won!!!";
      _battleOverPanel.SetActive(true);
    }
  }

  private void HandleGameStateUpdated(GameState gameState)
  {
    _gameState = gameState;
    _battleOverStats.text = $"Victories: {gameState.Victories} | Lives: {gameState.Lives}";
  }


  public void HandlePointerDown()
  {
    SceneManager.LoadScene(_gameState.Lives <= 0 || _gameState.Victories >= 3 ? "MenuScene" : "DraftScene");
  }
}
