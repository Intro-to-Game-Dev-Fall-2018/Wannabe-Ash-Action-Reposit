using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class BallController : MonoBehaviour {

	public SpriteRenderer SR;
	public Rigidbody2D RB;
	public float speed;
	public Text PointsTextLeft;
	public Text PointsTextRight;
	public Text WinText;
	public AudioClip hitSound;
    public AudioSource source;
    public AudioClip cheers;
    public AudioSource source2;

    private int LeftScorecount;
	private int RightScorecount;
	private int LeftHitCount;
	private int RightHitCount;

	private Boolean AlreadyHitR;
	private Boolean AlreadyHitL;
	
	void Start ()
	{
		RB = GetComponent<Rigidbody2D>();
        source.clip = hitSound;
        source2.clip = cheers;
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
        source2.Play();
		if(Input.GetKey(KeyCode.Space)){
			vel.y = -speed;
            RB.velocity = vel;
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
			source.PlayOneShot(hitSound);
            Debug.Log("Hit");
			RB.velocity = vel;
			AlreadyHitL = true;
			AlreadyHitR = false;
			Debug.Log("This is the LHC: " + LeftHitCount);			
			
			if (AlreadyHitL && LeftHitCount < 3)
			{
				LeftHitCount++;
                RightHitCount = 0;
			}

			else
			{
				LeftHitCount = 0;
				RightHitCount = 0;
				AlreadyHitL = false;
				transform.position = new Vector3(3, 0, -6);
                RightScorecount++;
				SetPointsText();	
			}

			
			
		}
		
		if (other.gameObject.tag == "RightPlayer")
		{
			Vector2 vel = RB.velocity;
            source.PlayOneShot(hitSound);
			RB.velocity = vel;
			AlreadyHitR = true;
			AlreadyHitL = false;
			Debug.Log("This is the RHC: " + RightHitCount);
			
			if (AlreadyHitR && RightHitCount < 3)
			{
				RightHitCount++;
                LeftHitCount = 0;
			}

			else
			{
				RightHitCount = 0;
				LeftHitCount = 0;
				transform.position = new Vector3(-3, 0, -6);
				LeftScorecount++;
				AlreadyHitR = false;
				SetPointsText();
			}

		}

		if (other.gameObject.tag == "L.Wall")
		{
			Vector2 vel = RB.velocity;
			vel.x = speed;
			RB.velocity = vel;
            source.PlayOneShot(hitSound);
        }

		if (other.gameObject.tag == "R.Wall")
		{
			Vector2 vel = RB.velocity;
			vel.x = -speed;
			RB.velocity = vel;
            source.PlayOneShot(hitSound);
        }

		if (other.gameObject.tag == "Ceiling")
		{
			Vector2 vel = RB.velocity;
			vel.y = -speed;
			RB.velocity = vel;
            source.PlayOneShot(hitSound);
        }

		if (other.gameObject.tag == "Net")
		{
			Debug.Log("This was AHL: " + AlreadyHitL);
			Debug.Log("This was AHR: " + AlreadyHitR);
            source.PlayOneShot(hitSound);

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
