using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<Obstacle> obstacles = new();
    [SerializeField] private float minSpawnCooldown = 1f;
    [SerializeField] private float maxSpawnCooldown = 2.5f;


    float GetRandomSpawnCooldown()
    {
        return Mathf.Round(Random.Range(minSpawnCooldown, maxSpawnCooldown) * 10f) / 10f;
    }

    private float spawnTimer = 0f;

    Vector3 spawnPosition;

    private void Start()
    {
        spawnTimer = GetRandomSpawnCooldown();
    }

    private void Update()
    {
        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
            return;
        }

        if (obstacles.Count == 0)
        {
            return;
        }

        spawnPosition = transform.position;

        float halfWidth = transform.localScale.x / 2f;
        spawnPosition.x += Random.Range(-halfWidth + 1f, halfWidth - 1f);

        Obstacle obstacle = obstacles[Random.Range(0, obstacles.Count)];

        Instantiate(obstacle, spawnPosition, Quaternion.identity);

        spawnTimer = GetRandomSpawnCooldown();
    }

}
