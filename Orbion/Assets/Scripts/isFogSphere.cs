﻿using UnityEngine;
using System.Collections;

public class isFogSphere : MonoBehaviour {

	private float timer;
	private float timerCD;
	private Rigidbody clone;

	public int fogCounter;
	public Rigidbody fog;
	private float scaler = 0.5f;


	// Use this for initialization
	void Start () {
		timer = 0.0f;
		timerCD = 10.0f;
		transform.localScale += new Vector3(fogCounter * scaler, fogCounter * scaler, fogCounter * scaler);

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(fogScript.fogCount);
		timer += Time.deltaTime;
		if(timer > timerCD){
			timer = 0.0f;
			popSphere();


		}
	
	}
	public void popSphere(){



		for(int i = 0; i <= fogCounter; i++){

			clone = Instantiate(fog, transform.position, Quaternion.identity) as Rigidbody;

		}
		Destroy(gameObject);
	}
	
}
