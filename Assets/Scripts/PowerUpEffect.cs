using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEffect : MonoBehaviour
{
    // Manager References
    private _GameManager GM;

    // Timers
    public Timer invulnerabilityTimer;
    public Timer doublePointsTimer;
    public float invulnerabilityDuration;
    public float doublePointsDuration;

    // Singleton
    private static PowerUpEffect _instance;
    public static PowerUpEffect Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this) { Destroy(gameObject); }
        else { _instance = this; }

        if (!invulnerabilityTimer)  { Debug.LogError("The powerup Handler can't find the Invulnerability Timer!"); }
        if (!doublePointsTimer)     { Debug.LogError("The powerup Handler can't find the Double Points Timer!"); }
    }

    private void Start()
    {
        GM = _GameManager.Instance;
        if (!GM) { Debug.LogError("UIManager can't find the GameManager!"); }
    }

    private void Update()
    {
        if (doublePointsTimer.GetCurrentTime() >= invulnerabilityDuration)
        {
            GM.GetScoreManager().SetDoublePoints(false);
            doublePointsTimer.StopTimer();
            doublePointsTimer.ResetTimer();
            GM.GetUIManager().UpdateIcons();
        }

        if (invulnerabilityTimer.GetCurrentTime() >= doublePointsDuration)
        {
            GM.GetPlayer().GetComponent<PlayerStats>().SetInvulnerability(false);
            invulnerabilityTimer.StopTimer();
            invulnerabilityTimer.ResetTimer();
            GM.GetUIManager().UpdateIcons();
        }
    }

    public void ExtraLife()
    {
        GM.GetPlayer().GetComponent<PlayerStats>().AddLife();
        GM.GetUIManager().UpdateIcons();
    }

    public void ExtraShield()
    {
        GM.GetPlayer().GetComponent<PlayerStats>().AddShield();
        GM.GetUIManager().UpdateIcons();
    }

    public void Invulnerability()
    {
        GM.GetPlayer().GetComponent<PlayerStats>().SetInvulnerability(true);

        if (invulnerabilityTimer.GetActive())
            invulnerabilityTimer.ResetTimer();
        else
            invulnerabilityTimer.StartTimer();

        GM.GetUIManager().UpdateIcons();

        // Start blinking
        GM.GetPlayer().GetComponent<BlinkingEffect>().ActivateBlinking(GM.GetPlayer().GetComponent<SpriteRenderer>(), 5.0f);
    }

    public void DoublePoints()
    {
        GM.GetScoreManager().SetDoublePoints(true);

        if (doublePointsTimer.GetActive())
            doublePointsTimer.ResetTimer();
        else
            doublePointsTimer.StartTimer();

        GM.GetUIManager().UpdateIcons();
    }
}
