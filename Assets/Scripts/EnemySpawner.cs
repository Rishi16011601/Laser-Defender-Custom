using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    [SerializeField] int noOfEnimiesDied = 0;
    [SerializeField] float timeForNextWave = 1.5f;

    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    
    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            //yield return
                StartCoroutine(SpawnAllEnemiesInWave(currentWave));
            yield return new WaitUntil(() => noOfEnimiesDied == waveConfigs[waveIndex].GetEnimies());
            yield return new WaitForSeconds(timeForNextWave);
            noOfEnimiesDied = 0;
            waveConfigs[waveIndex].LevelUp(1, 1);    }
    }
    
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for ( int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWayPoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    public void EnemyDied()
    {
        noOfEnimiesDied++;
    }
}
