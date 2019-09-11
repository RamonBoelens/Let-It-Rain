using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingEffect : MonoBehaviour
{
    // Activation
    private bool active;

    // Duration
    private float effectDuration;
    private float blinkingInterval;

    // Timers
    private float currentTime;
    private float intervalTimer;

    // Object
    private SpriteRenderer spriteRenderer;
    private Color newColor;
    private float alphaLevel;

    // Interval settings
    public float maxInterval = 1.4f;
    public float minInterval = 0.4f;

    private void Awake()
    {
        // Make sure the blinking isn't activated on start
        active = false;
    }

    private void Update()
    {
        // Check if the blinking is active
        if (active)
        {
            // Increase the timer
            currentTime += Time.deltaTime;

            // Check if the effect should be deactivated
            if (currentTime >= effectDuration) { DeactivateBlinking(); return; }

            // Increase the interval timer and make the blinking interval shorter
            intervalTimer += Time.deltaTime;
            blinkingInterval = minInterval / currentTime * (maxInterval - minInterval);

            // Check if the object should blink
            if (intervalTimer >= blinkingInterval)
            {
                // Blink
                if (spriteRenderer.color.a == 0.8f)
                    alphaLevel = 0.33f;
                else
                    alphaLevel = 0.8f;

                // Apply the new alpha value to the object
                newColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alphaLevel);
                spriteRenderer.color = newColor;

                // Reset the intverval timer
                intervalTimer = 0.0f;
            }
        }
    }

    public void ActivateBlinking(SpriteRenderer blinkingObject, float duration)
    {
        // Set the object and duration
        spriteRenderer = blinkingObject;
        effectDuration = duration;

        // Set the current alpha to slightly transparent
        alphaLevel = 0.33f;
        newColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.g, alphaLevel);
        spriteRenderer.color = newColor;

        // Active the blinking
        active = true;
    }

    public void DeactivateBlinking()
    {
        // Deactivate the blinking
        active = false;

        // Return the alpha color to full visibility
        alphaLevel = 1;
        newColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.g, alphaLevel);
        spriteRenderer.color = newColor;

        // Reset the timer
        ResetTimer();
    }

    private void ResetTimer()
    {
        // Set both of the timers back to 0.
        currentTime = 0.0f;
        intervalTimer = 0.0f;
    }
}
