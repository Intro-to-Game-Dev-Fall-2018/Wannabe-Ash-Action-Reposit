using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamTwoMover : MonoBehaviour {

	public float speed;
	public float jumpPower;
	public int JumpsLeft = 1;
	private Rigidbody2D rb2d;
	private GameObject team;
	
	
	
	// Use this for initialization
	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector2 vel = rb2d.velocity;
		if (Input.GetKey(KeyCode.D))
		{
			vel.x = speed;	
		}
		else if (Input.GetKey(KeyCode.A))
		{
			vel.x = -speed;
		}
		else
		{
			vel.x = 0;
		}

		if (Input.GetKey(KeyCode.W) && JumpsLeft > 0)
		{
			JumpsLeft--;
			vel.y = jumpPower;
		}

		rb2d.velocity = vel;
		//Debug.Log(rb2d.velocity);
	}

	void RawMovement()
	{
		if (Input.GetKey(KeyCode.D))
		{
			transform.position += new Vector3(speed, 0, 0);
		}

		if (Input.GetKey(KeyCode.A))
		{
			transform.position += new Vector3(-speed, 0, 0);
		}

		if (Input.GetKey(KeyCode.W))
		{
			transform.position += new Vector3(0, jumpPower, 0);
		}
	}

	
	void AddForceMove(){
		if (Input.GetKey (KeyCode.D)) {
			rb2d.AddForce (new Vector2 (speed, 0),ForceMode2D.Force);
		}
		if (Input.GetKey (KeyCode.A)) {
			rb2d.AddForce (new Vector2 (-speed, 0),ForceMode2D.Force);
		}
		if (Input.GetKey (KeyCode.W)) {
			rb2d.AddForce (new Vector2 (0, jumpPower),ForceMode2D.Impulse);
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "L.Floor")
		{
			JumpsLeft = 2;
		}
	}
}
