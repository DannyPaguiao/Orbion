﻿using UnityEngine;
using System.Collections;

public class Killable : MonoBehaviour {
	/*
	Gives prefabs HP, they can take damage, and die
	*/
	//currHP tracks health, baseHP is given unless changed in the prefab
	public int currHP = 100; 
	public int baseHP = 100;
	public GameObject deathTarget;

	//used to check if we should call the event when a building is hurt
	public Buildable buildScript;


	// Set HP to default
	void Start () {
		buildScript = gameObject.GetComponent<Buildable>();
		currHP = baseHP;
	}


	void Update () {
		//Debug.Log("Obj: " + this.gameObject.name + "CurrHP = " + currHP);
	}


	// Updates HP based on damage taken, calls kill() on dead objects
	public void damage (int dmg) {
		if (buildScript != null) EventManager.OnDamagingBuilding(this);
		currHP -= dmg;
		if (currHP <= 0) kill();
		if(gameObject.GetComponent<IsDamagedEffect>() != null){
			gameObject.GetComponent<IsDamagedEffect>().addDamage();
		}
	}


	// Kills enemy or player
	void kill () {

		if(gameObject.tag == "Player"){
			ResManager.Reset();
			TechManager.Reset();
			Application.LoadLevel("scene1");
		}
		else{
			Destroy (gameObject);
			MetricManager.AddEnemiesKilled(1);
			//Debug.Log(MetricManager.getEnemiesKilled);
			if (deathTarget != null) {
				Vector3 temp = transform.position;
				temp.y += 4;
				Instantiate (deathTarget, temp, this.transform.rotation);
			}
		}
		//make death object
	}


	/// <summary>
	/// Heal the specified health.
	/// </summary>
	/// <param name="health">Health.</param>
	public void Heal(int health){
		currHP += health;
		if(currHP > baseHP)
			currHP = baseHP;
	}
}