using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "UIScriptableObject", menuName = "Scriptable Objects/UIScriptableObject")]
public class UIScriptableObject : ScriptableObject
{
  public void LoadScene(string sceneName)
  {
    SceneManager.LoadScene(sceneName);
  }

  public void Quit()
  {
    Debug.Log("QUIT!!!");
    Application.Quit();
  }

  public void Draw()
  {
    Debug.Log("DRAW!!!!");
    GameManager.Draw();
  }
}
