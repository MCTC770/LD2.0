using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

	[SerializeField] List<Transform> waypoints;
	[SerializeField][Range(1, 10)] float enemySpeed = 0.05f;
	[SerializeField] bool enableBackAndForthPattern = true;
	Transform nextWaypoint;
	int waypointCounter = 1;
	bool moveDirectionForward = true;
	float enemyMovementSpeed;

	// Use this for initialization
	void Start () {
		nextWaypoint = waypoints[waypointCounter];
		transform.position = waypoints[0].position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		enemyMovementSpeed = enemySpeed * Time.deltaTime;
		MovementPattern();
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
		if (waypointCounter + 1 > waypoints.Count)
		{
			waypointCounter = waypoints.Count - 1;
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
}
