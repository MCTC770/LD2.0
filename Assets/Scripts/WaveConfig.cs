using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
//[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class WaveConfig : ScriptableObject {

	[SerializeField] GameObject enemyPrefab;
	[SerializeField] GameObject pathPrefab;
	[SerializeField] float timeBetweenSpawns = 0.5f;
	[SerializeField] float spawnRandomFactor = 0.3f;
	[SerializeField] int numberOfEnemies = 5;
	[SerializeField] float moveSpeed = 2f;
	List<Transform> waypointList;

	public GameObject GetEnemyPrefab()
	{
		return enemyPrefab;
	}

	public List<Transform> GetWaypoints()
	{
		Transform[] waveWaypoints = pathPrefab.GetComponentsInChildren<Transform>();
		foreach (Transform child in pathPrefab.transform)
		{
			waypointList.Add(child);
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
}
