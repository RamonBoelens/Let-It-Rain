using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndStats : MonoBehaviour
{
    public TextMeshProUGUI endScore, endTime;

    public void UpdateEndStats()
    {
        endScore.text = "End score: " + GameManager.Instance.GetScore();
        endTime.text = "End time: " + GameManager.Instance.GetTimer();
    }
}
