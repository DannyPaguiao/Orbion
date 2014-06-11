﻿// PURPOSE:  Handles the checking of Mission Objectives from Tech Manager and reflects that on the User Interface.  
// Mission 1:  Tutorial part two, walks player through standard core loop of the game of building a base and getting upgrades.  
using UnityEngine;
using System.Collections;

public class Mission1 : MonoBehaviour {
	public dfCheckbox _checkbox_1;
	public dfCheckbox _checkbox_2;
	public dfCheckbox _checkbox_3;
	public dfCheckbox _checkbox_4;
	public dfCheckbox _checkbox_5;
	public dfLabel _label_mission_clear;
	public dfLabel _label_paused;
	public dfLabel _label_dead;
	string collectString;
	public bool questComplete;
	public bool bossDefeated;

	// Use this for initialization
	void Start () {
		questComplete = false;
		bossDefeated = false;
		_checkbox_1.IsChecked = false;
		_checkbox_2.IsChecked = false;
		_checkbox_3.IsChecked = false;
		_checkbox_4.IsChecked = false;
		_checkbox_5.IsChecked = false;
	}
	
	// Update is called once per frame
	void Update () {



			if(TechManager.hasGenerator == true && TechManager.hasScatter == true && TechManager.hasMedbay == true && TechManager.hasWolves == true){
				questComplete = true;
				
			}

			collectString = string.Format("{0}", ResManager.Collectible);
			_checkbox_4.Label.Text = "Collect 20 Enemy Specimens: " + collectString;
			if(TechManager.hasGenerator){
				_checkbox_1.IsChecked = true;
			}
			if(TechManager.hasScatter){
				_checkbox_2.IsChecked = true;
			}
			if(TechManager.hasMedbay){
				_checkbox_3.IsChecked = true;
			}
			if(ResManager.Collectible >= ResManager.MaxCollectible){
				_checkbox_4.IsChecked = true;
			}
			if(TechManager.hasWolves){
				_checkbox_5.IsVisible = true;
				_checkbox_5.Label.Text = "Defeat the Alpha Wolf.";
				if(TechManager.hasBeatenWolf)
					_checkbox_5.IsChecked = true;
					bossDefeated = true;
			}

			if(TechManager.hasGenerator == true && TechManager.hasScatter == true && TechManager.hasMedbay == true /*&& ResManager.Collectible >= 30*/ && TechManager.hasWolves == true && TechManager.hasBeatenWolf == true){
				TechManager.missionComplete = true;
				_label_mission_clear.IsVisible = true;
				MetricManager.setCompletionTime(Time.time);
				MetricManager.calculateScore();
			}

			_label_paused.IsVisible = GameManager.paused;

			if(GameManager.PlayerDead){
				_label_dead.IsVisible = true;
			}
			else
				_label_dead.IsVisible = false;

		}





}
