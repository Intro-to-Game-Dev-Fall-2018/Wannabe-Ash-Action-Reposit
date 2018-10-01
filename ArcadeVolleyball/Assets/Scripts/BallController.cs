using System;
using System.Collections;
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
	private int LeftScorecount;
	private int RightScorecount;
	private int LeftHitCount;
	private int RightHitCount;
	private AudioSource source;
//	private float volLowRange = 0.5f;
//	private float volHighRange = 1.0f;
	private Boolean AlreadyHitR;
	private Boolean AlreadyHitL;
	
	void Start ()
	{
		RB = GetComponent<Rigidbody2D>();
		source = GetComponent<AudioSource>();
		transform.position = new Vector3(-3, 0, -6);
		WinText.text = "";
		LeftScorecount = 0;
		RightScorecount = 0;
		LeftHitCount = 0;
		RightHitCount = 0;
		AlreadyHitR = false;
		AlreadyHitL = false;
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
			transform.position = new Vector3(-3, 0, -6);
			RightScorecount++;
			LeftHitCount = 0;
			RightHitCount = 0;
			SetPointsText();
		}

		if (other.gameObject.tag == "R.Floor")
		{
			transform.position = new Vector3(3, 0, -6);
			LeftScorecount++;
			RightHitCount = 0;
			LeftHitCount = 0;
			SetPointsText();
		}

		if (other.gameObject.tag == "LeftPlayer")
		{
			Vector2 vel = RB.velocity;
			//vel.y = speed;
			//vel.x = speed;
			RB.velocity = vel;
			AlreadyHitL = true;
			Debug.Log("This is the LHC: " + LeftHitCount);			
			
			if (AlreadyHitL && LeftHitCount < 3)
			{
				Debug.Log("The L.Ball was hit");
				Debug.Log("This is the LHC: " + LeftHitCount);
				LeftHitCount++;
			}
			
			else if (!AlreadyHitL && LeftHitCount < 3)
			{
				Debug.Log("The R.Ball was hit");
				Debug.Log("This is the LHC: " + LeftHitCount);
				AlreadyHitL = false;
				RightHitCount = 0;
				LeftHitCount = 0;
			}

			else
			{
				LeftHitCount = 0;
				RightHitCount = 0;
				transform.position = new Vector3(3, 0, -6);
				RightScorecount++;
				SetPointsText();	
			}

			
			
		}
		
		if (other.gameObject.tag == "RightPlayer")
		{
			Vector2 vel = RB.velocity;
			//vel.y = speed;
			//vel.x = -speed;
			RB.velocity = vel;
			AlreadyHitR = true;
			Debug.Log("This is the RHC: " + RightHitCount);
			
			if (AlreadyHitR && RightHitCount < 3)
			{
				Debug.Log("The R.Ball was hit");
				Debug.Log("This is the RHC: " + RightHitCount);
				RightHitCount++;
			}
			
			else if (!AlreadyHitL && RightHitCount < 3)
			{
				Debug.Log("The L.Ball was hit");
				Debug.Log("This is the RHC: " + RightHitCount);
				LeftHitCount = 0;
				RightHitCount = 0;
				AlreadyHitR = false;
			}

			else
			{
				RightHitCount = 0;
				LeftHitCount = 0;
				transform.position = new Vector3(-3, 0, -6);
				LeftScorecount++;
				SetPointsText();
			}

		}

		if (other.gameObject.tag == "L.Wall")
		{
			Vector2 vel = RB.velocity;
			vel.x = speed;
			//vel.y = -speed;
			RB.velocity = vel;
		}

		if (other.gameObject.tag == "R.Wall")
		{
			Vector2 vel = RB.velocity;
			vel.x = -speed;
			//vel.y = -speed;
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
			vel.y = -speed;
			vel.x = -speed;
			RB.velocity = vel;
		}

	}
	
	void SetPointsText()
	{
		PointsTextLeft.text = "Score:" + LeftScorecount.ToString();
		PointsTextRight.text = "Score:" + RightScorecount.ToString();
		RB.velocity = new Vector2(0,0);
		if (LeftScorecount >= 10 || RightScorecount >=10)
		{
			WinText.text = "Victory!!!";
			RB.velocity = new Vector2(0,0);
		}
	}
}
