using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	[SerializeField] List<WaveConfig> waveConfigs;
	[SerializeField] int startingWave = 0;
	[SerializeField] bool repeatWavePattern = false;

	// Use this for initialization
	IEnumerator Start ()
	{
		do
		{
			yield return StartCoroutine(SpawnAllWaves());
		}
		while (repeatWavePattern);
	}

	IEnumerator SpawnAllWaves()
	{
		for (int i = 0; i < waveConfigs.Count; i++)
		{
			if (startingWave > i)
			{
				i = startingWave;
			}
			yield return SpawnAllEnemiesInWave(waveConfigs[i]);
		}
	}

	IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
	{
		for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
		{
			GameObject newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
			newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
			yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
