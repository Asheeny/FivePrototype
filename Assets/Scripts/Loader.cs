using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    private class LoadingMono : MonoBehaviour { }

    public enum Scene
    {
        Main_Menu,
        Level_Select,
        Level_1,
        Level_2,
        Level_3,
        Level_4,
        Level_5,
        Transition,
    }

    private static Action onLoaderCallback;

    public static void Load(Scene scene)
    {
        onLoaderCallback = () =>
        {
            GameObject loadingObj = new GameObject("The loading object");
            loadingObj.AddComponent<LoadingMono>().StartCoroutine(LoadingAsync(scene));
        };
        SceneManager.LoadScene(Scene.Transition.ToString());
    }

    private static IEnumerator LoadingAsync(Scene scene)
    {
        yield return new WaitForSeconds(1.2f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene.ToString());

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
