using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameManager : MonoBehaviour
{
    // References
    private PlayerController player;
    private _ScoreManager scoreManager;
    private _UIManager UIManager;
    private _MenuManager menuManager;
    private PowerUpEffect powerUpHandler;

    // End Scores
    private float endScore;
    private float endTime;

    // Singleton
    private static _GameManager _instance;
    public static _GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this) { Destroy(gameObject); }
        else { _instance = this; }
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        scoreManager = _ScoreManager.Instance;
        UIManager = _UIManager.Instance;
        menuManager = _MenuManager.Instance;
        powerUpHandler = PowerUpEffect.Instance;

        if (!player)         { Debug.LogError("The GameManager can't find the player!"); }
        if (!scoreManager)   { Debug.LogError("The GameManager can't find the score manager!"); }
        if (!UIManager)      { Debug.LogError("The GameManager can't find the UI manager!"); }
        if (!menuManager)    { Debug.LogError("The GameManager can't find the menu manager!"); }
        if (!powerUpHandler) { Debug.LogError("The GameManager can't find the power up handler!"); }

        GetComponent<Timer>().StartTimer();
    }

    public void Hit()
    {
        player.GetComponent<PlayerStats>().Hit();
        UIManager.UpdateIcons();
    }

    public void PowerUp(string powerUp)
    {
        if (powerUp == "ExtraLife")             { powerUpHandler.ExtraLife(); }
        else if (powerUp == "ExtraShield")      { powerUpHandler.ExtraShield(); }
        else if (powerUp == "Invulnerability")  { powerUpHandler.Invulnerability(); }
        else if (powerUp == "DoublePoints")     { powerUpHandler.DoublePoints(); }

        else { Debug.LogWarning("Can't find the power up: " + powerUp); }
    }

    public void EndGame()
    {
        endScore = scoreManager.GetScore();
        endTime = GetComponent<Timer>().GetCurrentTime();

        menuManager.LoadScene(4);
    }

    public float GetEndScore()
    {
        return endScore;
    }

    public float GetEndTime()
    {
        return endTime;
    }

    public _ScoreManager GetScoreManager()
    {
        return scoreManager;
    }

    public _UIManager GetUIManager()
    {
        return UIManager;
    }

    public PlayerController GetPlayer()
    {
        return player;
    }

    public PowerUpEffect GetPowerUpHandler()
    {
        return powerUpHandler;
    }
}