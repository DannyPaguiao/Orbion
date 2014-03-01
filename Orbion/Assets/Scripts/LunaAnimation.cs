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

	bool MoveKeyStay(){
		return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||  Input.GetKey(KeyCode.S) ||  Input.GetKey(KeyCode.D);
	}

	// Update is called once per frame
	void Update () {

		
		

		//Want to update it if a key was let go else it won't 
		//change a diagonal if one of the keys were let go
		if ( MoveKeyDown() || MoveKeyUp() || Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)){
		Vector3 newRotation = Vector3.zero;

		if (Input.GetKey(KeyCode.W)){
			newRotation += Vector3.forward;
			
				//animation.CrossFade("Run");
		}
		if (Input.GetKey(KeyCode.S)){
			newRotation += Vector3.back;
			
				//animation.CrossFade("Run");

		}
		
		if (Input.GetKey(KeyCode.A)){
			newRotation += Vector3.left;

				//animation.CrossFade("Run");

		}
		if (Input.GetKey(KeyCode.D)){
			newRotation += Vector3.right;

				//animation.CrossFade("Run");

		}

		if (Input.GetMouseButton(0)){
			animation.CrossFade("Shooting");
			Vector3 temp;
			temp = Utility.GetMouseWorldPos(transform.position.y) - transform.position;
			newRotation = temp;
		}

		
		if (MoveKeyStay()) animation.CrossFade("Run");

		//only change our position if there is an update to it
		if (newRotation != Vector3.zero) transform.forward = newRotation;
		}

		if (GameManager.AvatarContr.lightconeObj.light.enabled){
			transform.forward = Utility.GetMouseWorldPos(transform.position.y) - transform.position;
			//animation.CrossFade("Shooting");
		}

		if (! MoveKeyStay() && ! Input.GetMouseButton(0) ){
			animation.CrossFade("Idle");
	 }

	
	}
}
