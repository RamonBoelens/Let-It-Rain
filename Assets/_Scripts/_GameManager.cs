using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameManager : MonoBehaviour
{
    private PlayerController player;
    private _ScoreManager scoreManager;
    private _UIManager uiManager;

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
}
