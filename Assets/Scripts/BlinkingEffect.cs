using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingEffect : MonoBehaviour
{
    private bool active;
    private float effectDuration;
    private float effectInterval;
    private float intervalTimer;
    private float currentTime;
    private Color objectColor;

    private void Awake()
    {
        active = false;
    }

    private void Update()
    {
        if (active)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= effectDuration) { DeactivateBlinking(); return; }

            intervalTimer += Time.deltaTime;
            effectInterval = (effectDuration - currentTime) / 2;

            if (intervalTimer >= effectInterval)
            {
                // Blink
                if (objectColor.a != 1)
                    objectColor.a = 0.33f;
                else
                    objectColor.a = 1;

                intervalTimer = 0.0f;
            }
        }
    }

    public void ActivateBlinking(Color color, float duration)
    {
        active = true;
        effectDuration = duration;
        objectColor = color;
    }

    public void DeactivateBlinking()
    {
        active = false;
        objectColor.a = 1;
        ResetTimer();
    }

    private void ResetTimer()
    {
        currentTime = 0.0f;
        intervalTimer = 0.0f;
    }
}
