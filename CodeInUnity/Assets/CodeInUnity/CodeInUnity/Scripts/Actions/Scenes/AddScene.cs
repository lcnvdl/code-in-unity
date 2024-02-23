using UnityEngine.SceneManagement;

namespace CodeInUnity.Scripts.Actions.Scenes
{
  public class AddScene : ActionScript
  {
    public string sceneName;

    protected override void Run()
    {
      SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
  }
}