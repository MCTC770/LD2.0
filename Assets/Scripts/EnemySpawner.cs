using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	[SerializeField] List<WaveConfig> waveConfigs;
	int startingWave = 0;

	// Use this for initialization
	void Start () {
		var currentWave = waveConfigs[startingWave];
		StartCoroutine(SpawnAllEnemiesInWave(currentWave));
	}

	IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
	{
		for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
		{
			Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
			yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
