using UnityEngine.SceneManagement;

namespace CodeInUnity.Scripts.Actions.Scenes
{
  public class UnloadScene : ActionScript
  {
    public string sceneName;

    protected override void Run()
    {
      SceneManager.UnloadSceneAsync(sceneName);
    }
  }
}