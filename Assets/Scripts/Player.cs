using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] PlayerProjectileBehavior playerLaserProjectile;
	[SerializeField] [Range(1f, 10f)] float moveSpeed = 2.5f;
	[SerializeField] bool updateMoveBoundaries = false;
	[SerializeField] [Range(0.001f, 1f)] float shootFrequency;
	[SerializeField] PlayerProjectileBehavior[] projectileStorageArray;
	int projectileStorageCount = 8;
	int projectileCounter = 0;
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
		ProjectileStorageArrayCreator();
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

	void ProjectileStorageArrayCreator()
	{
		projectileStorageArray = new PlayerProjectileBehavior[] 
		{ playerLaserProjectile, playerLaserProjectile, playerLaserProjectile, playerLaserProjectile,
			playerLaserProjectile, playerLaserProjectile, playerLaserProjectile, playerLaserProjectile,
			playerLaserProjectile, playerLaserProjectile, playerLaserProjectile, playerLaserProjectile,
			playerLaserProjectile, playerLaserProjectile, playerLaserProjectile, playerLaserProjectile };
		for(int i = 0; i < projectileStorageCount; i++)
		{
			projectileStorageArray[i].gameObject.name = "Player Laser " + i;
			Instantiate(projectileStorageArray[i], new Vector3(-100, 0, 0), Quaternion.identity);
		}
		print(projectileStorageArray[1]);
	}

	IEnumerator ProjectileDelay()
	{
		yield return new WaitForSeconds(shootFrequency);
		{
			GameObject.Find("Player Laser " + projectileCounter + "(Clone)").GetComponent<PlayerProjectileBehavior>().move = true;
			GameObject.Find("Player Laser " + projectileCounter + "(Clone)").transform.position = this.gameObject.transform.position;
			if (projectileCounter < projectileStorageCount - 1)
			{
				projectileCounter += 1;
			} else
			{
				projectileCounter = 0;
			}
			timer = 0f;
			nextProjectile = true;
		}
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
		if (Input.GetAxis("Fire1") > 0 && nextProjectile)
		{
			nextProjectile = false;
			StartCoroutine(ProjectileDelay());
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

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.name == "Enemy")
		{
			Destroy(gameObject);
		}
	}
}
