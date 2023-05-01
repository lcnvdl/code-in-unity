using UnityEngine;

namespace CodeInUnity.Core.System
{
    public static class Injector
    {
        private static IInjectorInstance globalInstance;

        public static IInjectorInstance Global
        {

            get
            {
                if (globalInstance == null)
                {
                    var goInstance = GameObject.Find("InjectionManager_Global");

                    if (goInstance != null)
                    {
                        globalInstance = goInstance.GetComponent<InjectionManagerScript>();
                    }

                    if (globalInstance == null)
                    {
                        var go = (goInstance == null) ? new GameObject("InjectionManager_Global") : goInstance;
                        go.isStatic = true;
                        globalInstance = go.AddComponent<InjectionManagerScript>();

                        if (Application.isPlaying)
                        {
                            GameObject.DontDestroyOnLoad(go);
                        }
                    }
                }

                return globalInstance;
            }

            set
            {
                globalInstance = value;
            }
        }

        private static IInjectorInstance sceneInstance;

        public static IInjectorInstance Instance
        {
            get
            {
                if (sceneInstance == null)
                {
                    var goInstance = GameObject.Find("InjectionManager_SceneInstance");

                    if (goInstance != null)
                    {
                        sceneInstance = goInstance.GetComponent<InjectionManagerScript>();
                    }

                    if (globalInstance == null)
                    {
                        var go = (goInstance == null) ? new GameObject("InjectionManager_SceneInstance") : goInstance;
                        go.isStatic = true;
                        sceneInstance = go.AddComponent<InjectionManagerScript>();
                    }
                }

                return sceneInstance;
            }

            set
            {
                sceneInstance = value;
            }
        }
    }
}
