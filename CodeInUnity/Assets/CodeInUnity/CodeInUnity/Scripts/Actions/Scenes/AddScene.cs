using UnityEngine.SceneManagement;

public class AddScene : ActionScript
{
    public string sceneName;

    protected override void Run()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}
