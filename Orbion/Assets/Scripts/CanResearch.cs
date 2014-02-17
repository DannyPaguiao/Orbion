﻿using UnityEngine;
using System.Collections;

public class CanResearch : MonoBehaviour {

	private bool menuUp = false;

	// Use this for initialization
	void Start () {
	
	}
	

	//Returns true only if we have enough lumen, energy, and we satisfy the prereqs
	bool MeetsRequirement(Tech theUpgr){
		if( !TechManager.IsTechAvaliable( theUpgr)) return false;
		if (ResManager.Lumen < TechManager.GetUpgradeLumenCost( theUpgr)) return false;

		float neededMaxEnergy = ResManager.UsedEnergy + TechManager.GetUpgradeEnergyCost( theUpgr);
		if ( neededMaxEnergy > ResManager.MaxEnergy) return false;

		return true;
	}


	//Calls Research and spends resources
	void DoResearch(Tech theUpgr){
		ResManager.RmLumen( TechManager.GetUpgradeLumenCost( theUpgr));
		ResManager.AddUsedEnergy( TechManager.GetUpgradeEnergyCost( theUpgr));
		TechManager.Research( theUpgr);
	}


	void OnGUI() {
		if(menuUp){
			GetComponent<CanShoot>().ResetFiringTimer();
			GUI.Box(new Rect (10,10,100,90), "Research Menu");

			if(GUI.Button(new Rect(20,40,80,20), "Scattershot")) {
				if(MeetsRequirement(Tech.scatter)){
					DoResearch(Tech.scatter);
					menuUp = false;
				}
			}

			if(GUI.Button(new Rect(20,60,80,20), "Orbshot")) {
				if(MeetsRequirement(Tech.orbshot)){
					DoResearch(Tech.orbshot);

					//Just slapping it here for now until we have a way to to manage bullets for the player.
					//Should make an event manager to broadcast that a upgrade was researched later
					GameManager.AvatarContr.shootScript.bullet = GameManager.AvatarContr.orbBullet;
					menuUp = false;
				}
			}


			if(GUI.Button(new Rect(20,80,80,20), "Light Grenade")) {
				if(MeetsRequirement(Tech.lightGrenade)){
					DoResearch(Tech.lightGrenade);
					menuUp = false;
				}
			}

			if(GUI.Button(new Rect(20,100,80,20), "Bullet Absorber")) {
				if(MeetsRequirement(Tech.bulletAbsorber)){
					DoResearch(Tech.bulletAbsorber);
					menuUp = false;
				}
			}

			if(GUI.Button(new Rect(20,120,80,20), "Clip Size")) {
				if(MeetsRequirement(Tech.clipSize)){
					DoResearch(Tech.clipSize);

					//here until we have a event manager for upgrades
					GameManager.AvatarContr.shootScript.clipSize += 10 * TechManager.GetUpgradeLv(Tech.clipSize);
					menuUp = false;
				}
			}



		}
	
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.V)){
			menuUp = !menuUp;
		}
	
	}
}
