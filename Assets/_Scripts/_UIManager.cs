using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _UIManager : MonoBehaviour
{
    // Manager References
    private _GameManager GM;

    /*
     * Multiplier Objects
     */
    public TextMeshProUGUI MultiplierText, ScoreText;
    private float totalMultiplier, score;

    private void Start()
    {
        GM = _GameManager.Instance;

        if (!GM)
            Debug.LogError("UIManager can't find the GameManager!");
    }

    private void Update()
    {
        // Get the latest multiplier and score
        if (GM)
        {
            totalMultiplier = GM.GetScoreManager().GetTotalMultiplier();
            score = GM.GetScoreManager().GetScore();
        }

        UpdateText();
    }

    private void UpdateText()
    {
        MultiplierText.text = "Multiplier: " + totalMultiplier.ToString("F2") + "x";
        ScoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
    }
}
