using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float xMin, xMax;
    private float spawnCooldown = 0.3f;

    public GameObject enemyPf;

    private void Start()
    {
        Camera cam = Camera.main;

        // Get the screen width
        xMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
    }

    private void Update()
    {
        if (spawnCooldown <= 0.0f)
        {
            SpawnObject();
            ResetCooldown(0.3f);
        }
        
        else
        {
            spawnCooldown =- Time.deltaTime;
        }
    }

    private void SpawnObject()
    {
        Vector2 SpawnPos = new Vector2(Random.Range(xMin, xMax / 2), transform.position.y);
        var go = Instantiate(enemyPf, new Vector3(SpawnPos.x, SpawnPos.y, 0), transform.rotation);
        go.transform.SetParent(gameObject.transform);

        float fallSpeed = Random.Range(3.0f, 5.0f);


        go.GetComponent<EnemyController>().fallSpeed = fallSpeed;
    }

    private void ResetCooldown(float time)
    {
        spawnCooldown = time;
    }
}
