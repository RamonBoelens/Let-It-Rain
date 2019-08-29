using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawnable Objects")]
    public GameObject[] enemyPrefabs;
    public GameObject[] powerUpPrefabs;

    [Header("Spawn Rate")]
                    public AnimationCurve spawnRate;
    [Range(0, 100)] public int powerUpPercentage;

    [Header("Attributes")]
    [Range(1.0f, 3.5f)] public float enemyMinFallSpeed;
    [Range(3.5f, 6.0f)] public float enemyMaxFallSpeed;
    [Space(10)]
    [Range(0.25f, 0.75f)] public float enemyMinSize;
    [Range(1.60f, 2.10f)] public float enemyMaxSize;
    [Space(20)]
    [Range(1.0f, 6.0f)] public float powerUpFallSpeed;

    // Spawn rate
    private Timer timer;
    private float currentSpawnRate;
    private float spawnCooldown;

    // Object weight
    private int maximumWeigth;
    private int powerUpWeight;
    private int enemyWeight;

    // Spawn area
    private float xMin, xMax;

    private void Start()
    {
        // Set References
        Camera cam = Camera.main;
        timer = GetComponent<Timer>();

        // Calculate the weight of the spawnable objects
        CalculateWeight();

        // Get the screen width
        xMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        // Start the timer
        timer.StartTimer();

        // Set the current spawn rate to be equal to the timer and reset the cooldown to match the spawn rate
        currentSpawnRate = spawnRate.Evaluate(timer.GetCurrentTime());
        ResetCooldown(currentSpawnRate);
    }

    private void Update()
    {
        // Set the current spawn rate to match the Animation Curve
        currentSpawnRate = spawnRate.Evaluate(timer.GetCurrentTime());

        // Check if we are able to spawn a new object
        if (spawnCooldown <= 0.0f)
        {
            SpawnObject();
            ResetCooldown(currentSpawnRate);
        }
        // Else decrease to cooldown time
        else { spawnCooldown -= Time.deltaTime; }
    }

    private void SpawnObject()
    {
        // Select a random integer up to the maximum weigth value
        int i = Random.Range(0, maximumWeigth);

        // Spawn Enemy
        if (i < enemyWeight)
        {
            // Get a random shape
            int enemyID = Random.Range(0, enemyPrefabs.Length);

            // Randomize stats
            float fallSpeed = Random.Range(enemyMinFallSpeed, enemyMaxFallSpeed);
            float size      = Random.Range(enemyMinSize, enemyMaxSize);

            // Randomize Spawn Position
            Vector2 SpawnPos = new Vector2(Random.Range(xMin, xMax), transform.position.y);

            // Instantiate the object
            GameObject go = Instantiate(enemyPrefabs[enemyID], new Vector3(SpawnPos.x, SpawnPos.y, 0), transform.rotation);
            go.transform.SetParent(gameObject.transform);

            // Apply the stats
            go.GetComponent<Gravity>().SetFallSpeed(fallSpeed);
            go.transform.localScale = new Vector3(size, size, 0);
        }

        // Spawn Power Up
        else
        {
            // Get a random power up
            int powerUpID = Random.Range(0, powerUpPrefabs.Length);

            // Randomize SpawnPosition
            Vector2 SpawnPos = new Vector2(Random.Range(xMin, xMax), transform.position.y);

            // Instantiate the object
            GameObject go = Instantiate(powerUpPrefabs[powerUpID], new Vector3(SpawnPos.x, SpawnPos.y, 0), transform.rotation);
            go.transform.SetParent(gameObject.transform);

            // Apply stats
            go.GetComponent<Gravity>().SetFallSpeed(powerUpFallSpeed);
        }
    }

    // Calculating the weight of each object to spawn based of the power up spawn percentage. Maximum weight = 100
    private void CalculateWeight()
    {
        powerUpWeight = powerUpPercentage;
        enemyWeight = 100 - powerUpPercentage;

        maximumWeigth = powerUpWeight + enemyWeight;
    }

    // Match the cooldown to the current spawn rate
    private void ResetCooldown(float time)
    {
        spawnCooldown = time;
    }
}
