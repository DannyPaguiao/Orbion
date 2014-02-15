﻿using UnityEngine;
using System.Collections;

public class LunaAnimation : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	bool MoveKeyDown(){
		return Input.GetKeyDown( KeyCode.W) || Input.GetKeyDown( KeyCode.S) || Input.GetKeyDown( KeyCode.A) || Input.GetKeyDown( KeyCode.D);
	}

	bool MoveKeyUp(){
		return Input.GetKeyUp( KeyCode.W) || Input.GetKeyUp( KeyCode.S) || Input.GetKeyUp( KeyCode.A) || Input.GetKeyUp( KeyCode.D);
	}

	// Update is called once per frame
	void Update () {

		//Want to update it if a key was let go else it won't 
		//change a diagonal if one of the keys were let go
		if ( MoveKeyDown() || MoveKeyUp() ){
		Vector3 newRotation = Vector3.zero;

		if (Input.GetKey(KeyCode.W))
			newRotation += Vector3.forward;
		if (Input.GetKey(KeyCode.S))
			newRotation += Vector3.back;
		
		if (Input.GetKey(KeyCode.A))
			newRotation += Vector3.left;
		if (Input.GetKey(KeyCode.D))
			newRotation += Vector3.right;

		//only change our position if there is an update to it
		if (newRotation != Vector3.zero) transform.forward = newRotation;
		}





	if(Input.GetKeyUp("w")||Input.GetKeyUp("a")||Input.GetKeyUp("d") || Input.GetKeyUp("s")) {
			animation.CrossFade("Idle");
	 }

	
	}
}
