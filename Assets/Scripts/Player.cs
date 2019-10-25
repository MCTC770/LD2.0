using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] PlayerProjectileBehavior playerLaserProjectile;
	[SerializeField] [Range(1f, 10f)] float moveSpeed = 2.5f;
	[SerializeField] bool updateMoveBoundaries = false;
	[SerializeField] [Range(0.01f, 1000f)] float shootFrequency;
	bool nextProjectile = true;
	float timer = 0f;
	float minXPos;
	float maxXPos;
	float minYPos;
	float maxYPos;

	// Use this for initialization
	void Start ()
	{
		MoveBoundaries();
		QualitySettings.vSyncCount = 1;
	}

	// Update is called once per frame
	void Update () {
		if (updateMoveBoundaries)
		{
			MoveBoundaries();
		}
		ShipMovement();
		ShootProjectile();
	}

	IEnumerator ProjectileDelay()
	{
		yield return new WaitForSeconds(shootFrequency);
		print("Coroutine Done");
		nextProjectile = true;
	}

	void ShipMovement()
	{
		float moveVelocity = moveSpeed * Time.deltaTime;

		if (Input.GetAxis("Vertical") > 0)
		{
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + moveVelocity, gameObject.transform.position.z);
		}
		else if (Input.GetAxis("Vertical") < 0)
		{
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - moveVelocity, gameObject.transform.position.z);
		}

		if (Input.GetAxis("Horizontal") > 0)
		{
			gameObject.transform.position = new Vector3(gameObject.transform.position.x + moveVelocity, gameObject.transform.position.y, gameObject.transform.position.z);
		}
		else if (Input.GetAxis("Horizontal") < 0)
		{
			gameObject.transform.position = new Vector3(gameObject.transform.position.x - moveVelocity, gameObject.transform.position.y, gameObject.transform.position.z);
		}

		ClampPlayerPosition();
	}

	void ShootProjectile()
	{
		//timer += Time.deltaTime;

		if (!nextProjectile)
		{
			StartCoroutine(ProjectileDelay());
		}

		/*if (timer >= shootFrequency)
		{
			nextProjectile = true;
		}*/

		if (Input.GetAxis("Fire1") > 0 && nextProjectile)
		{
			Instantiate
				(playerLaserProjectile, new Vector3(
					this.gameObject.transform.position.x, 
					this.gameObject.transform.position.y, 
					this.gameObject.transform.position.z), 
					Quaternion.identity);
			timer = 0f;
			nextProjectile = false;
		}
	}

	private void ClampPlayerPosition()
	{
		gameObject.transform.position = new Vector3(
			Mathf.Clamp(gameObject.transform.position.x, minXPos, maxXPos),
			Mathf.Clamp(gameObject.transform.position.y, minYPos, maxYPos),
			gameObject.transform.position.z);
	}

	private void MoveBoundaries()
	{
		minXPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
		maxXPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
		minYPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
		maxYPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log(collision.collider.name);
	}
}
