using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TeamOneMover : MonoBehaviour
{

	public float speed;
	public float jumpPower;
	public int JumpsLeft = 1;
	private Rigidbody2D rb2d;
	private GameObject team;
	public Sprite Jumper;

	
	// Use this for initialization
	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector2 vel = rb2d.velocity;
		if (Input.GetKey(KeyCode.RightArrow))
		{
			vel.x = speed;	
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			vel.x = -speed;
		}
		else
		{
			vel.x = 0;
		}

		if (Input.GetKey(KeyCode.UpArrow) && JumpsLeft > 0)
		{
			JumpsLeft--;
			vel.y = jumpPower;
		}

		rb2d.velocity = vel;
		//Debug.Log(rb2d.velocity);
	}

	void RawMovement()
	{
		if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.position += new Vector3(speed, 0, 0);
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.position += new Vector3(-speed, 0, 0);
		}

		if (Input.GetKey(KeyCode.UpArrow))
		{
			transform.position += new Vector3(0, jumpPower, 0);
		}
	}

	
	void AddForceMove(){
		if (Input.GetKey (KeyCode.RightArrow)) {
			rb2d.AddForce (new Vector2 (speed, 0),ForceMode2D.Force);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			rb2d.AddForce (new Vector2 (-speed, 0),ForceMode2D.Force);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			rb2d.AddForce (new Vector2 (0, jumpPower),ForceMode2D.Impulse);
		}
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        Vector2 vel = rb2d.velocity;
        if (coll.gameObject.tag == "R.Floor")
        {
            JumpsLeft = 2;
        }

        if (coll.gameObject.tag == "R.Wall")
        {
            vel.y = -speed;

        }

        rb2d.velocity = vel;

        //if (coll.gameObject.tag != "R.Floor")
        //{
        //    this.GetComponent<SpriteRenderer>().sprite = Jumper;
        //}
        Debug.Log(coll.gameObject.tag);
        if (coll.gameObject.tag == "PowerUp")
        {
            Debug.Log("triggered");
            //b.speed *= 2;
        }

    }
	
}
