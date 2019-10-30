using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject {

	[SerializeField] GameObject enemyPrefab;
	[SerializeField] GameObject pathPrefab;
	[SerializeField] float timeBetweenSpawns = 0.5f;
	[SerializeField] float spawnRandomFactor = 0.3f;
	[SerializeField] int numberOfEnemies = 5;
	[SerializeField] float moveSpeed = 2f;
	[SerializeField] bool enableBackAndForthPattern = false;
	[SerializeField] List<Transform> waypointList;

	public GameObject GetEnemyPrefab()
	{
		return enemyPrefab;
	}

	public void ResetWaypoints()
	{
		waypointList.Clear();
	}

	public List<Transform> GetWaypoints()
	{
		if (waypointList.Count < pathPrefab.transform.childCount)
		{
			Transform[] waveWaypoints = pathPrefab.GetComponentsInChildren<Transform>();
			foreach (Transform child in pathPrefab.transform)
			{
				waypointList.Add(child);
			}
		}
		return waypointList;
	}

	public float GetTimeBetweenSpawns()
	{
		return timeBetweenSpawns;
	}

	public float GetSpawnRandomFactor()
	{
		return spawnRandomFactor;
	}

	public int GetNumberOfEnemies()
	{
		return numberOfEnemies;
	}

	public float GetMoveSpeed()
	{
		return moveSpeed;
	}

	public bool GetEnableBackAndForthPattern()
	{
		return enableBackAndForthPattern;
	}
}
