using UnityEngine.SceneManagement;

public class LoadScene : ActionScript
{
    public string sceneName;

    protected override void Run()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
