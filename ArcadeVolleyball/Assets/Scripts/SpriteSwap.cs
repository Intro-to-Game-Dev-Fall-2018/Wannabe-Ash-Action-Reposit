using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwap : MonoBehaviour {

	public Sprite Jumper;
	
	// Use this for initialization
	void Start ()
	{
		
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		//this.GetComponent<SpriteRenderer>().sprite = Jumper;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "R.Floor")
		{
			this.GetComponent<SpriteRenderer>().sprite = Jumper;
		}
	}
}
