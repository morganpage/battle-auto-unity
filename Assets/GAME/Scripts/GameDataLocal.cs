using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDataLocal", menuName = "Scriptable Objects/GameDataLocal")]
public class GameDataLocal : ScriptableObject
{
  public List<Character> characters = new List<Character>();

  public List<Team> teams = new List<Team>();//List of all the teams ever played in the game
}
