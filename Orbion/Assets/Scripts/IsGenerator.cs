﻿using UnityEngine;
using System.Collections;

public class IsGenerator : MonoBehaviour {
	public int energyGeneration = 50;


	// Use this for initialization
	void Start () {
		ResManager.AddMaxEnergy(energyGeneration);
	}

	void OnDestroy() {
		ResManager.RmMaxEnergy(energyGeneration);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
