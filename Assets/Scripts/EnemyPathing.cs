using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

	[SerializeField] int enemyHP = 1;

	WaveConfig waveConfig;
	List<Transform> waypoints;
	bool enableBackAndForthPattern;
	Transform nextWaypoint;
	int waypointCounter = 1;
	bool moveDirectionForward = true;
	float enemyMovementSpeed;

	// Use this for initialization
	void Start () {
		waveConfig.ResetWaypoints();
		waypoints = waveConfig.GetWaypoints();
		nextWaypoint = waypoints[waypointCounter];
		transform.position = waypoints[0].position;
		enableBackAndForthPattern = waveConfig.GetEnableBackAndForthPattern();
	}
	
	// Update is called once per frame
	void Update ()
	{
		enemyMovementSpeed = waveConfig.GetMoveSpeed() * Time.deltaTime;
		MovementPattern();
	}

	public void SetWaveConfig(WaveConfig theWave)
	{
		waveConfig = theWave;
	}

	private void MovementPattern()
	{
		if (transform.position != nextWaypoint.position)
		{
			transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.position, enemyMovementSpeed);
		}
		else if (enableBackAndForthPattern)
		{
			BackAndForthMovementPattern();
		}
		else
		{
			OneWayMovementPattern();
		}
	}

	private void OneWayMovementPattern()
	{
		waypointCounter += 1;
		if (waypointCounter >= waypoints.Count)
		{
			waypointCounter = waypoints.Count - 1;
		}
		if (waypointCounter == waypoints.Count - 1 && this.gameObject.transform.position == waypoints[waypoints.Count - 1].transform.position)
		{
			Destroy(gameObject);
		}
		nextWaypoint = waypoints[waypointCounter];
	}

	private void BackAndForthMovementPattern()
	{
		if (waypointCounter + 1 == waypoints.Count && moveDirectionForward)
		{
			moveDirectionForward = false;
		}
		if (transform.position == waypoints[0].position && !moveDirectionForward)
		{
			Debug.Log("is true");
			moveDirectionForward = true;
		}
		if (moveDirectionForward)
		{
			waypointCounter += 1;
			nextWaypoint = waypoints[waypointCounter];
		}
		else
		{
			waypointCounter -= 1;
			nextWaypoint = waypoints[waypointCounter];
		}
	}

	/*public void ReduceEnemyHP(int damage)
	{
		enemyHP -= damage;
		if (enemyHP <= 0)
		{
			Destroy(gameObject);
		}
	}*/
}
