using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameManager : MonoBehaviour
{
    // References
    private PlayerController player;
    private _ScoreManager scoreManager;
    private _UIManager UIManager;

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

        if (!player)        { Debug.LogError("The GameManager can't find the player!"); }
        if (!scoreManager)  { Debug.LogError("The GameManager can't find the score manager!"); }
        if (!UIManager)     { Debug.LogError("The GameManager can't find the UI manager!"); }
        
        GetComponent<Timer>().StartTimer();
    }

    public void Hit()
    {
        player.GetComponent<PlayerStats>().Hit();
        UIManager.UpdateIcons();
    }

    public void PowerUp(string powerUp)
    {
        if (powerUp == "ExtraLife")
        { GetComponent<PowerUpEffect>().ExtraLife(); }
        else if (powerUp == "ExtraShield")
        { GetComponent<PowerUpEffect>().ExtraShield(); }
        else { Debug.LogWarning("Can't find the power up: " + powerUp); }
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
}