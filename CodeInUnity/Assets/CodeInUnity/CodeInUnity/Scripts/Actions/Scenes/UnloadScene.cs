using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadScene : ActionScript
{
    public string sceneName;

    protected override void Run()
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
