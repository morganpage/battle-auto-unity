using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BattleAuto
{
  public Team playerTeam;
  public Team enemyTeam;
  public List<BattleState> BattleStates = new List<BattleState>();

  public BattleAuto()
  {
    BattleCharacter.OnBattleActionEvent += LogBattleActionEvent;
  }

  public void Battle(Team player, Team enemy)
  {
    BattleStates.Clear();
    this.playerTeam = player;
    this.enemyTeam = enemy;
    LogBattleActionEvent("BattleStart");
    while (playerTeam.BattleCharacters.Count > 0 && enemyTeam.BattleCharacters.Count > 0)
    {
      BattleRound(playerTeam.BattleCharacters[0], enemyTeam.BattleCharacters[0]);
    }
    if (playerTeam.BattleCharacters.Count == 0 && enemyTeam.BattleCharacters.Count == 0)
    {
      LogBattleActionEvent("BattleDraw");
      return;
    }
    if (playerTeam.BattleCharacters.Count == 0)
    {
      LogBattleActionEvent("BattleLost");
      return;
    }
    if (enemyTeam.BattleCharacters.Count == 0)
    {
      LogBattleActionEvent("BattleWon");
      return;
    }

  }

  private void LogBattleActionEvent(string action)
  {
    BattleState battleState = new BattleState(new Team(playerTeam), new Team(enemyTeam));
    battleState.Action = action;
    BattleStates.Add(battleState);
  }

  void BattleRound(BattleCharacter player, BattleCharacter enemy)
  {
    if (player.attack > enemy.attack)
    {
      player.Attack(enemy);
      enemy.Attack(player);
    }
    else
    {
      enemy.Attack(player);
      player.Attack(enemy);
    }
    RemoveZeroHealthCharacter();
  }

  void RemoveZeroHealthCharacter()
  {
    List<BattleCharacter> defeatedCharacters = new List<BattleCharacter>();
    foreach (BattleCharacter bc in playerTeam.BattleCharacters)
    {
      if (bc.health <= 0) defeatedCharacters.Add(bc);
    }
    foreach (BattleCharacter bc in enemyTeam.BattleCharacters)
    {
      if (bc.health <= 0) defeatedCharacters.Add(bc);
    }
    foreach (BattleCharacter bc in defeatedCharacters)
    {
      playerTeam.BattleCharacters.Remove(bc);
      enemyTeam.BattleCharacters.Remove(bc);
      bc.Defeated();
    }
  }
}
