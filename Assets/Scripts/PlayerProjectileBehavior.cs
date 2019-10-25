using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileBehavior : MonoBehaviour {

	[SerializeField] float moveSpeed = 10f;
	[SerializeField] float maxLifeTime = 2f;
	float projectileLifeTime = 0f;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		projectileLifeTime += Time.deltaTime;
		if (projectileLifeTime >= maxLifeTime)
		{
			Destroy(gameObject);
		}
		float moveVelocity = moveSpeed * Time.deltaTime;
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + moveVelocity, gameObject.transform.position.z);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.name == "Enemy")
		{
			Destroy(collision.gameObject);
			Destroy(gameObject);
		}
	}
}
