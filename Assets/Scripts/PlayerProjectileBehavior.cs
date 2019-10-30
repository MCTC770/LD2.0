using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileBehavior : MonoBehaviour {

	[SerializeField] float moveSpeed = 10f;
	[SerializeField] float maxLifeTime = 2f;
	public bool move = false;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		//if (transform.position.y > 6f)
		if (Camera.main.ViewportToWorldPoint(new Vector3(0, 1.1f, 0)).y < transform.position.y)
		{
			move = false;
		}
		float moveVelocity = moveSpeed * Time.deltaTime;
		if (move)
		{
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + moveVelocity, gameObject.transform.position.z);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.GetComponents<EnemyPathing>().Length >= 1)
		{
			Destroy(collision.gameObject);
			transform.position = new Vector3(-100, 0, 0);
			move = false;
		}
	}
}
