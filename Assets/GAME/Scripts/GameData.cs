using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class GameData
{
  public static List<Character> characters = new List<Character>();

  public static async Task Init()
  {
    if (characters.Count > 0) return;
    //characters = Resources.Load<GameDataLocal>("GameDataLocal").characters;
    GameDataFlow gameDataFlow = Resources.Load<GameDataFlow>("GameDataFlow");
    await gameDataFlow.Init();
    characters = gameDataFlow.characters;
    Debug.Log($"Characters loaded: {characters.Count}");
  }

  public static string GetRandomCharacterName(int maxTier)
  {
    //Init();
    var tierCharacters = characters.FindAll(x => x.tier <= maxTier);
    int randomTier = Random.Range(0, tierCharacters.Count);
    return tierCharacters[randomTier].name;
  }

  public static Character GetCharacter(string name)
  {
    //Init();
    return characters.Find(x => x.name == name);
  }

  public static void AddTeamToDatabase(Team team, int turn)
  {
    List<Team> teams = Resources.Load<GameDataLocal>("GameDataLocal").teams;
    team.Turn = turn;
    teams.Add(team);
  }

  public static Team GetTeamFromDatabase(int turn)
  {
    Debug.Log($"GetTeamFromDatabase Turn: {turn}");
    List<Team> teams = Resources.Load<GameDataLocal>("GameDataLocal").teams;
    List<Team> teamsFilteredByTurn = teams.FindAll(x => x.Turn == turn);
    //Now return a random team from this filtered list
    int randomIndex = Random.Range(0, teamsFilteredByTurn.Count);
    return new Team(teamsFilteredByTurn[randomIndex]);
  }
}
