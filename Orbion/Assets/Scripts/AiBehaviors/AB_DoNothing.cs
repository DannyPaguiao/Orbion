﻿using UnityEngine;
using System.Collections;

//Default behavior for AIs that do nothing
public class AB_DoNothing : AiBehavior {

	override public void OnBehaviorEnter(){
		//Debug.Log("Entering idle");
		//animation.CrossFade("Idle");
		return;
	}
	
	override public void OnBehaviorExit(){return;}
	
	override public void FixedUpdateAB(){return;}
	
	override public void UpdateAB(){
		if(GetComponent<IsEnemy>().enemyType != 0)
		animation.CrossFade("Idle");
	}
}
