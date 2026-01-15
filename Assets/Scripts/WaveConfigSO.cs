using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfig", menuName = "New WaveConfig")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float enemyMoveSpeed = 5f;
    [SerializeField] float timeBetweenEnemySpawns = 1f;
    [SerializeField] float enemySpawnVariance = 0f;
    [SerializeField] float minimumSpawnTime = 0.2f;


    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    public float GetEnemyMoveSpeed()
    {
        return enemyMoveSpeed;
    }

    public int GetEnemyCount()
    {
        return enemyPrefabs.Length;
    }



    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }


    public Transform[] GetWaypoints()
    {
        Transform[] waypoints = new Transform[pathPrefab.childCount];

        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathPrefab.GetChild(i);
        }
        return waypoints;
    }

    public float GetRandomEnemySpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - enemySpawnVariance, timeBetweenEnemySpawns + enemySpawnVariance);

        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}
