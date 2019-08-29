using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private bool active;
    private float time;

    private void Awake()
    {
        active = false;
    }

    private void Update()
    {
        if (active)
            time += Time.deltaTime;
    }

    public void StartTimer()
    {
        active = true;
        Debug.Log("Timer started!");
    }

    public void StopTimer()
    {
        active = false;
        Debug.Log("Timer stopped");
    }

    public void ResetTimer()
    {
        time = 0.0f;
        Debug.Log("Timer has been reset!");
    }

    public float GetCurrentTime()
    {
        return time;
    }
}
