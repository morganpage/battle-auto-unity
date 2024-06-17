using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Team
{
  public string Name;
  public int Turn; //Just used when saving / choising a previously played team
  public List<BattleCharacter> BattleCharacters = new List<BattleCharacter>();

  public Team(params BattleCharacter[] battleCharacters)
  {
    Name = string.Join("-", battleCharacters.Select(bc => bc.name));
    BattleCharacters = new List<BattleCharacter>(battleCharacters);
  }

  public Team(Team team)
  {
    Name = team.Name;
    foreach (BattleCharacter bc in team.BattleCharacters)
    {
      BattleCharacters.Add(bc.Clone());
    }
  }
}
