using System;
using UnityEngine;

[System.Serializable]
public class BattleCharacter : Character
{
  public static event Action<string> OnBattleActionEvent;
  public string UID;

  public BattleCharacter(Character character)
  {
    UID = System.Guid.NewGuid().ToString();
    this.name = character.name;
    this.description = character.description;
    this.tier = character.tier;
    this.attack = character.attack;
    this.health = character.health;
  }

  public BattleCharacter Clone()
  {
    BattleCharacter bc = new BattleCharacter(this);
    bc.UID = this.UID;
    return bc;
  }
  public void Attack(BattleCharacter target)
  {
    target.health -= this.attack;
    Debug.Log($"{ShortName} attacks {target.ShortName} for {attack} damage so {target.ShortName} health is now {target.health}");
    OnBattleActionEvent?.Invoke("Attack");
  }

  public void Defeated()
  {
    ///Any on defeated code put here
    OnBattleActionEvent?.Invoke("Defeated");
  }

  public string ShortName => $"{name}:{UID.Substring(0, 3)}";
}
