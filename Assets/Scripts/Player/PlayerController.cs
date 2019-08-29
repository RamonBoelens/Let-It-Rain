using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Manager References
    private _GameManager GM;

    private float xMin, xMax, yMin, yMax;
    private Vector3 mousePosition;

    private void Start()
    {
        GM = _GameManager.Instance;
        if (!GM) { Debug.LogError("UIManager can't find the GameManager!"); }

        Camera cam = Camera.main;

        // Get the screen size
        xMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = cam.ViewportToWorldPoint(new Vector3(0, 0.875f, 0)).y;
    }

    private void Update()
    {
        // Take the current mouse position
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Clamp the object inside the screen
        float xPos = Mathf.Clamp(mousePosition.x, xMin, xMax);
        float yPos = Mathf.Clamp(mousePosition.y, yMin, yMax);

        // Set the objects position
        transform.position = new Vector3 (xPos, yPos, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Checks what the player hit
        if (other.tag == "Enemy")
        {
            if (GetComponent<PlayerStats>().GetInvulnerability()) { Debug.Log("Player is invulnerable."); return; }
            GM.Hit();
        }
        else
        {
            GM.PowerUp(other.tag);
            Destroy(other.gameObject);
        }
    }

    public float GetVerticalMousePosition()
    {
        return ((transform.position.y + 5) * 1.14f) / 10;
    }
}
