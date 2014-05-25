﻿using UnityEngine;
using System.Collections;

public class Mission2Help : MonoBehaviour {
	public dfLabel tutorialLine;
	bool gotLumen = false;
	bool pressedB = false;
	bool fiveSecPassed = false;
	bool pressedV = false;
	float delay;
	public hasOverdrive overdriveScript;
	public DumbTimer timerScript;
	
	// Use this for initialization
	void Start () {
		timerScript = DumbTimer.New(5.0f, 1.0f);
		overdriveScript = GameManager.AvatarContr.GetComponent<hasOverdrive>();
		tutorialLine.IsVisible = true;
		delay = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if(!TechManager.hasGeyser){
			tutorialLine.Text = "Press E on a Light Geyser to fill Energy Core";

		}
		else if(TechManager.hasGeyser && timerScript.Finished() == false){
			tutorialLine.Text = "Deposit Charged Energy Core at Spacecraft";
			timerScript.Update();
		}
		else if(timerScript.Finished())
			tutorialLine.Text = "";
		




		if(overdriveScript.overdriveOn){
			if(!overdriveScript.overdriveActive)
				tutorialLine.Text = "Press SPACE to activate Overdrive!";
			
			if(Input.GetKeyDown(KeyCode.Space)){
				
				tutorialLine.Text = "";
			}
		}
		
	}
}
