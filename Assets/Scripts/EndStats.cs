using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndStats : MonoBehaviour
{
    public TextMeshProUGUI endScore, endTime;

    public void UpdateEndStats()
    {
        string minutes;
        string seconds;
        CalculateTime(GameManager.Instance.GetTimer(), out minutes, out seconds);

        endScore.text = "End score: " + GameManager.Instance.GetScore();
        if (minutes == "0")
            endTime.text = "You barely survived for " + seconds + " seconds.";
        else
            endTime.text = "You survived for " + minutes + ":" + seconds + "!";
    }

    public void CalculateTime(int timeInSeconds, out string minutes, out string seconds)
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
