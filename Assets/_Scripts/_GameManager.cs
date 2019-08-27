using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameManager : MonoBehaviour
{
    // References
    private PlayerController player;
    private _ScoreManager scoreManager;
    private _UIManager uiManager;

    // Info
    private int lives;
    private int shields;

    // Singleton
    private static _GameManager _instance;
    public static _GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if (!player)
            Debug.LogError("The GameManager can't find the player!");

        scoreManager = _ScoreManager.Instance;

        if (!player)
            Debug.LogError("The GameManager can't find the score manager!");

        GetComponent<Timer>().StartTimer();
    }

    public _ScoreManager GetScoreManager()
    {
        return scoreManager;
    }

    public PlayerController GetPlayer()
    {
        return player;
    }

    public void AddShield()
    {
        if (shields >= 3)
        {
            Debug.Log("Maximum shields reached. Couldn't add 1");
            return;
        }

        shields++;
    }

    public void AddLife()
    {
        if (lives >= 5)
        {
            Debug.Log("Maximum lives reached. Couldn't add 1");
            return;
        }

        lives++;
    }

    public void SubstractLife()
    {
        if (shields > 0)
        {
            shields--;
            return;
        }

        lives--;

        CheckDeath();
    }

    public void CheckDeath()
    {
        if (lives > 0)
            return;
    }
}