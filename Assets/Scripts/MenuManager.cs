using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject gameCanvas, endStateCanvas;

    private static MenuManager _instance;
    public static MenuManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;
    }

    public void RestartGame()
    {
        GameManager.Instance.ResetGame();
        EnableGameCanvas(true);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EnableGameCanvas(bool enable)
    {
        gameCanvas.SetActive(enable);
        endStateCanvas.SetActive(!enable);
    }
}
