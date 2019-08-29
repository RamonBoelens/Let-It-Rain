using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private float fallSpeed = 2.5f;

    private void LateUpdate()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    public void SetFallSpeed(float speed)
    {
        fallSpeed = speed;
    }
}
