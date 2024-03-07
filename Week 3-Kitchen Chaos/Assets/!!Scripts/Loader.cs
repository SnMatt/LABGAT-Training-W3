using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public static string _targetScene;

    public static void LoadScene(string sceneName)
    {
        Loader._targetScene = sceneName;

        SceneManager.LoadScene("Loading");


    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(_targetScene);
    }
}
