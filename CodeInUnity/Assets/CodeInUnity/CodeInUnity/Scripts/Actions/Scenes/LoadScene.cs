using UnityEngine.SceneManagement;

namespace CodeInUnity.Scripts.Actions.Scenes
{
  public class LoadScene : ActionScript
  {
    public string sceneName;

    protected override void Run()
    {
      SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
  }
}