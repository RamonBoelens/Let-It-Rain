using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Manager References
    private _GameManager GM;

    [Header("Initial Setup")]
    [Range(1, 5)] public int startingLives;
    [Range(0, 5)] public int startingShields;

    private int lives, shields;
    private bool invulnerable;

    private void Awake()
    {
        lives   = startingLives;
        shields = startingShields;
    }

    private void Start()
    {
        GM = _GameManager.Instance;
        if (!GM) { Debug.LogError("UIManager can't find the GameManager!"); }

        invulnerable = false;
    }

    public void Hit()
    {
        if (shields > 0) { shields--; return; }

        lives--;
    }

    public void AddLife()
    {
        if (lives >= 5) { Debug.Log("Maximum lives reached. Couldn't add a life."); return; }

        lives++;
    }

    public void AddShield()
    {
        if (shields >= 5) { Debug.Log("Maximum shields reached. Couldn't add a shield."); return; }

        shields++;
    }

    public void SetInvulnerability(bool active)
    {
        invulnerable = active;
    }

    public int GetLives()
    {
        return lives;
    }

    public int GetShields()
    {
        return shields;
    }

    public bool GetInvulnerability()
    {
        return invulnerable;
    }
}
