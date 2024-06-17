using System;
using UnityEngine;

[System.Serializable]
public class BattleState
{
  public static Action<BattleState> OnBattleRender;
  public Team playerTeam;
  public Team enemyTeam;
  public string Action; //Attack, BattleWon, BattleLost

  public BattleState(Team player, Team enemy)
  {
    this.playerTeam = player;
    this.enemyTeam = enemy;
  }

  public void RenderBattle()
  {
    OnBattleRender?.Invoke(this);
  }

  public BattleCharacter GetBattleCharacter(string uid)
  {
    BattleCharacter bc = playerTeam.BattleCharacters.Find(x => x.UID == uid);
    if (bc == null)
    {
      bc = enemyTeam.BattleCharacters.Find(x => x.UID == uid);
    }
    return bc;
  }

  public bool Exists(string uid)
  {
    return playerTeam.BattleCharacters.Exists(x => x.UID == uid) || enemyTeam.BattleCharacters.Exists(x => x.UID == uid);
  }
}
