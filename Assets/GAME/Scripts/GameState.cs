using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState
{
  public static Action<GameState> OnGameStateUpdated;
  public int Mana = 11;
  public int Turn = 1;
  public int Victories = 0;
  public int Lives = 3;
  public int Tier { get { return ((Turn - 1) / 2) + 1; } } //turn 1 - tier 1,turn 3 - tier 2
  private string[] hand;//Array of character names held in hand
  private Dictionary<int, BattleCharacter> field = new Dictionary<int, BattleCharacter>();

  public string[] Hand
  {
    get => hand;
    set
    {
      hand = value;
      OnGameStateUpdated?.Invoke(this);
    }
  }

  public Dictionary<int, BattleCharacter> Field
  {
    get => field;
    set
    {
      field = value;
      OnGameStateUpdated?.Invoke(this);
    }
  }

  public void Draw()
  {
    Turn++;
    Mana = 11;
    OnGameStateUpdated?.Invoke(this);
  }
  public void Lost()
  {
    Turn++;
    Mana = 11;
    Lives--;
    OnGameStateUpdated?.Invoke(this);
  }
  public void Won()
  {
    Turn++;
    Mana = 11;
    Victories++;
    OnGameStateUpdated?.Invoke(this);
  }
}
