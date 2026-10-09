using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    WaveConfigSO currentWave;

    [SerializeField] WaveConfigSO[] waveConfigs;
    [SerializeField] float timeBetweenWaves = 2f;
    [SerializeField] bool isLooping = true;



    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // coroutine


    IEnumerator SpawnEnemies()
    {

        do
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;

                for (int i = 0; i < wave.GetEnemyCount(); i++)
                {
                    Instantiate(wave.GetEnemyPrefab(i), wave.GetStartingWaypoint().position, Quaternion.identity, transform);

                    yield return new WaitForSeconds(wave.GetRandomEnemySpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }

        } while (isLooping);

    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

}
