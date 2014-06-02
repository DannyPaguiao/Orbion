﻿// Level 2 Mission Functionality, checks if player is within range of the Ship and deposits the full core that the player harvested from Light Geysers.
using UnityEngine;
using System.Collections;

public class isLunaShip : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			if(ResManager.LGEnergy >= ResManager.LGMaxEnergy){
				//Debug.Log (ResManager.LGCoreCharges);
				ResManager.AddLGCoreCharge(1);
				ResManager.ResetLGContainer();			
			}
		}


	}
}
