using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Spawnable GameObject
    public GameObject[] enemyPf;

    // Spawn rate
    public AnimationCurve spawnRate;
    private float timer;
    private float currentSpawnRate;
    private float spawnCooldown;

    // Spawn Area
    private float xMin, xMax;

    private void Start()
    {
        Camera cam = Camera.main;

        // Get the screen width
        xMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        currentSpawnRate = spawnRate.Evaluate(timer);
        ResetCooldown(currentSpawnRate);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        currentSpawnRate = spawnRate.Evaluate(timer);

        if (spawnCooldown <= 0.0f)
        {
            SpawnObject();
            ResetCooldown(currentSpawnRate);

        } else {
            spawnCooldown -= Time.deltaTime;
        }
    }

    private void SpawnObject()
    {
        // Get a random shape
        int enemyID = Random.Range(0, enemyPf.Length);

        // Randomize stats
        float fallSpeed = Random.Range(3.0f, 5.0f);
        float size = Random.Range(0.25f, 2.0f);

        // Randomize Spawn Position
        Vector2 SpawnPos = new Vector2(Random.Range(xMin, xMax), transform.position.y);

        // Instantiate object
        GameObject go = Instantiate(enemyPf[enemyID], new Vector3(SpawnPos.x, SpawnPos.y, 0), transform.rotation);
        go.transform.SetParent(gameObject.transform);

        // Apply the stats
        go.GetComponent<EnemyController>().fallSpeed = fallSpeed;
        go.transform.localScale = new Vector3(size, size, 0);
    }

    private void ResetCooldown(float time)
    {
        spawnCooldown = time;
    }
}
