using UnityEngine;

public class DraftScene : MonoBehaviour
{
  async void Start()
  {
    await GameData.Init();
    GameManager.Draw();
  }

  [ContextMenu("AddTeamToDatabase")]
  void AddTeamToDatabase()
  {
    GameManager.AddTeamToDatabase();
  }


}
