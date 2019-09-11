using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _MenuManager : MonoBehaviour
{
    // Singleton
    private static _MenuManager _instance;
    public static _MenuManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this) { Destroy(gameObject); }
        else { _instance = this; }
    }

    public void LoadScene(int buildIndex)
    {
        if (buildIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(buildIndex);
        else
            Debug.LogError("Can't load a scene with the build index of " + buildIndex + "!");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
