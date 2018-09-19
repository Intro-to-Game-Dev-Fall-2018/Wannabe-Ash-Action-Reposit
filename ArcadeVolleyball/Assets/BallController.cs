﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

	public SpriteRenderer SR;
	public Rigidbody2D RB;
	public float speed;
	public Text PointsTextLeft;
	public Text PointsTextRight;
	public Text WinText;
	public AudioClip hitSound;
	private int Leftcount;
	private int Rightcount;
	private AudioSource source;
	private float volLowRange = 0.5f;
	private float volHighRange = 1.0f;
	
	void Start ()
	{
		RB = GetComponent<Rigidbody2D>();
		source = GetComponent<AudioSource>();
		transform.position = new Vector2(-3, 0);
		WinText.text = "";
		Leftcount = 0;
		Rightcount = 0;
		SetPointsText();
	}
	
	void Update () {
		
		Vector2 vel = RB.velocity;
		
		if(Input.GetKey(KeyCode.Space)){
			vel.y = -speed;
			RB.velocity = vel;
			source.PlayOneShot(hitSound, 1F);
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		
		if (other.gameObject.tag == "L.Floor")
		{
			transform.position = new Vector2(-3, 0);
			Rightcount += 1;
			SetPointsText();
		}

		if (other.gameObject.tag == "R.Floor")
		{
			transform.position = new Vector2(3, 0);
			Leftcount += 1;
			SetPointsText();
		}

		if (other.gameObject.tag == "LeftPlayer")
		{
			Vector2 vel = RB.velocity;
			vel.y = -speed;
			vel.x = speed;
			RB.velocity = vel;
		}
		if (other.gameObject.tag == "RightPlayer")
		{
			Vector2 vel = RB.velocity;
			vel.y = -speed;
			vel.x = -speed;
			RB.velocity = vel;
		}

		if (other.gameObject.tag == "L.Wall")
		{
			Vector2 vel = RB.velocity;
			vel.x = speed;
			vel.y = -speed;
			RB.velocity = vel;
		}

		if (other.gameObject.tag == "R.Wall")
		{
			Vector2 vel = RB.velocity;
			vel.x = -speed;
			vel.y = -speed;
			RB.velocity = vel;
		}

		if (other.gameObject.tag == "Ceiling")
		{
			Vector2 vel = RB.velocity;
			vel.y = -speed;
			RB.velocity = vel;
		}

		if (other.gameObject.tag == "Net")
		{
			Vector2 vel = RB.velocity;
			vel.x = -speed;
			vel.y = -speed;
			RB.velocity = vel;
		}

	}
	
	void SetPointsText()
	{
		PointsTextLeft.text = "Score:" + Leftcount.ToString();
		PointsTextRight.text = "Score:" + Rightcount.ToString();
		RB.velocity = new Vector2(0,0);
		if (Leftcount >= 10 || Rightcount >=10)
		{
			WinText.text = "Victory!!!";
			RB.velocity = new Vector2(0,0);
		}
	}
}