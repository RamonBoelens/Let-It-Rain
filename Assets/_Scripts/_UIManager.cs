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
    public TextMeshProUGUI totalMultiplierText, timeMultiplierText, heightMultiplierText, doublePointsMultiplierText;
    private float totalMultiplier, timeMultiplier, heightMultiplier, doublePointsMultiplier;

    private void Start()
    {
        GM = _GameManager.Instance;

        if (!GM)
            Debug.LogError("UIManager can't find the GameManager!");
    }

    private void Update()
    {
        // Update the multipliers
        if (GM)
        {
            GM.GetScoreManager().GetMultipliers(out totalMultiplier, out timeMultiplier, out heightMultiplier, out doublePointsMultiplier);
        }

        UpdateText();
    }

    private void UpdateText()
    {
        totalMultiplierText.text = "Current Total Multiplier: " + totalMultiplier.ToString("F2") + "x";
        timeMultiplierText.text = "Current Time Multiplier: " + timeMultiplier.ToString("F2") + "x";
        heightMultiplierText.text = "Current Height Multiplier: " + heightMultiplier.ToString("F2") + "x";
        doublePointsMultiplierText.text = "Double Points Multiplier " + doublePointsMultiplier + "x";
    }
}
