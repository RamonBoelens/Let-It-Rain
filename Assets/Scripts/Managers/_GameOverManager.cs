using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI statsText;

    private void Start()
    {
        _GameManager GM = _GameManager.Instance;

        UpdateStats(GM.GetEndTime(), GM.GetEndScore());
    }

    public void UpdateStats(float endTime, float endScore)
    {
        string minutes;
        string seconds;
        CalculateTime(endTime, out minutes, out seconds);

        endScore = Mathf.FloorToInt(endScore);

        if (minutes == "0")
            statsText.text = "You barely survived for " + seconds + " seconds. \n" +
                             "This scores you a total of " + endScore + " points.";
        else
            statsText.text = "You survived for " + minutes + ":" + seconds + "! \n" +
                             "This scores you a total of " + endScore + " points.";
    }

    private void CalculateTime(float timeInSeconds, out string minutes, out string seconds)
    {
        int minutesInt = Mathf.FloorToInt(timeInSeconds / 60);
        int secondsInt = Mathf.RoundToInt(timeInSeconds % 60);

        minutes = minutesInt.ToString();
        seconds = secondsInt.ToString();

        if (minutesInt > 0 && secondsInt < 10)
        {
            seconds = "0" + secondsInt.ToString();
        }
    }
}
