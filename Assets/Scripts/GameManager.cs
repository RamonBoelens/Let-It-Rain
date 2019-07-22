using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    float multiplier = 1.0f;
    float timer = 0.0f;
    float score = 0.0f;

    public TextMeshProUGUI scoreText, timerText, multiplierText;

    private void Update()
    {
        // Update the timer
        timer += Time.deltaTime;
        UpdateTimerText();

        // Update the multiplier
        multiplier = timer / 10.0f + 1;
        multiplier = Mathf.FloorToInt(multiplier);
        UpdateMultiplierText();

        // Update the score
        score += 10 * Time.deltaTime * multiplier;
        UpdateScoreText();
    }

    private void UpdateTimerText()
    {
        timerText.text = "Timer: " + Mathf.FloorToInt(timer);
    }

    private void UpdateMultiplierText()
    {
        multiplierText.text = "Multiplier: " + multiplier + "x";
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + Mathf.FloorToInt(score);
    }
}
