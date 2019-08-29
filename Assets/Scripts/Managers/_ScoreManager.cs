using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ScoreManager : MonoBehaviour
{
    // GameManager Reference
    private _GameManager GM;

    private float totalMultiplier;
    private float timeMultiplier;
    private float heightMultiplier;
    private float doublePointsMultiplier = 1;

    public  float scorePerSecond = 10.0f;
    private float score = 0.0f;

    private static _ScoreManager _instance;
    public static _ScoreManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GM = _GameManager.Instance;

        if (!GM)
            Debug.LogError("ScoreManager can't find the GameManager!");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMultipliers();
        AddScore();
    }

    private void AddScore()
    {
        score += scorePerSecond * Time.deltaTime * totalMultiplier;
    }

    private void UpdateMultipliers()
    {
        // Time Multiplier
        if (GM)
        {
            timeMultiplier = GM.GetComponent<Timer>().GetCurrentTime() / 10.0f;
        }

        // Height Multiplier
        if (GM)
        {
            heightMultiplier = GM.GetPlayer().GetVerticalMousePosition() * 2;
        }

        // Double Points Multiplier

        // Total Multiplier
        totalMultiplier = timeMultiplier + heightMultiplier + doublePointsMultiplier;
    }

    public float GetTotalMultiplier()
    {
        return totalMultiplier;
    }

    public float GetScore()
    {
        return score;
    }
}
