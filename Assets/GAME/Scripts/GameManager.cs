using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameManager
{

  private static GameState _gameState = new GameState();
  private const int maxHandSize = 3;//const implies static
  private const int drawCost = 1;
  private const int playFromHandCost = 3;

  public static void Draw()
  {
    Debug.Log("Drawing a card to your hand");
    if (_gameState.Lives <= 0 || _gameState.Victories >= 3)
    {
      Debug.Log("Do a Reset here");
      _gameState = new GameState();
    }
    if (_gameState.Mana < drawCost) return;
    List<string> hand = new List<string>();
    while (hand.Count < maxHandSize)
    {
      string randomCharacterName = GameData.GetRandomCharacterName(_gameState.Tier);
      Debug.Log($"Drew a {randomCharacterName}");
      hand.Add(randomCharacterName);
    }
    _gameState.Mana = _gameState.Mana - drawCost;
    _gameState.Hand = hand.ToArray();
    Debug.Log($"Hand: {string.Join(", ", _gameState.Hand)}");
    //OnGameStateUpdated?.Invoke(_gameState);
  }

  public static void PlayFromHand(int handIndex, int fieldIndex)
  {
    Debug.Log($"PlayFromHand handIndex:{handIndex} to fieldIndex:{fieldIndex}");
    if (_gameState.Mana < playFromHandCost) return;
    if (fieldIndex == -1) return;
    if (_gameState.Field.ContainsKey(fieldIndex)) return;
    string characterName = _gameState.Hand[handIndex];
    Debug.Log($"Playing {characterName}");
    List<string> hand = new List<string>(_gameState.Hand);
    hand.RemoveAt(handIndex);
    _gameState.Hand = hand.ToArray();
    Character character = GameData.GetCharacter(characterName);
    Dictionary<int, BattleCharacter> field = new Dictionary<int, BattleCharacter>(_gameState.Field);
    field[fieldIndex] = new BattleCharacter(character);
    _gameState.Mana = _gameState.Mana - playFromHandCost;
    _gameState.Field = field;
  }

  public static void MoveCharacter(int fromIndex, int toIndex)
  {
    Debug.Log($"MoveCharacter fromIndex:{fromIndex} to toIndex:{toIndex}");
    if (!_gameState.Field.ContainsKey(fromIndex) || fromIndex == toIndex) return;
    if (_gameState.Field.ContainsKey(toIndex))
    {
      Debug.Log("Swapping");
      Dictionary<int, BattleCharacter> field = new Dictionary<int, BattleCharacter>(_gameState.Field);
      BattleCharacter temp = field[toIndex];
      field[toIndex] = field[fromIndex];
      field[fromIndex] = temp;
      _gameState.Field = field;
    }
    else
    {
      Debug.Log("Moving");
      Dictionary<int, BattleCharacter> field = new Dictionary<int, BattleCharacter>(_gameState.Field);
      field[toIndex] = field[fromIndex];
      field.Remove(fromIndex);
      _gameState.Field = field;
    }
  }

  public static List<BattleState> Battle() //Do a Battle and update the gameState with the outcome
  {
    BattleAuto battleAuto = new BattleAuto();
    BattleCharacter[] playerBattleCharacters = _gameState.Field.Values.ToArray();
    Team playerTeam = new Team(playerBattleCharacters);
    Team enemyTeam = GameData.GetTeamFromDatabase(_gameState.Turn);
    battleAuto.Battle(new Team(playerTeam), enemyTeam);
    BattleState battleState = battleAuto.BattleStates[battleAuto.BattleStates.Count - 1];
    switch (battleState.Action)
    {
      case "BattleWon":
        _gameState.Won();
        break;
      case "BattleLost":
        _gameState.Lost();
        break;
      default:
        _gameState.Draw();
        break;
    }
    return battleAuto.BattleStates;
  }

  public static void AddTeamToDatabase()
  {
    GameData.AddTeamToDatabase(new Team(_gameState.Field.Values.ToArray()), _gameState.Turn);
  }
}
