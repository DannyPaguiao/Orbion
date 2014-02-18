﻿using UnityEngine;
using System.Collections;

public class CanShoot : MonoBehaviour {

	//the object to shoot
	public Rigidbody bullet;

	//the speed at which to shoot the object
	public float bullet_speed = 200.0F;	

	//the cooldown in seconds between shots
	public float firingRate = 1F;

	//range of attack
	public float projectileStartPosition = 1.0F;	

	public AudioClip enemyShotSound;

	//used to keep track of our shooting cooldown
	protected float firingTimer = 0.0F;
	
	//holds a reference to the bullet that will be made
	private Rigidbody clone;

	// Variable bullet spawn height for diff users
	public Vector3 bulletHeight;

	// effect to play when you shoot
	public GameObject shootEffect;



	//sets the proportion of completion for the firingTimer
	//e.g. 0.5 = 50% finished with cooldown
	public void SetFiringTimer(float ratio){
		firingTimer = firingRate * ratio;
	}
	
	//Sets the FiringTimer to the beginning of the cooldown count
	public void ResetFiringTimer(){
		SetFiringTimer(0);
	}
	
	//returns if we have elapsed the firing cooldown
	public bool FinishCooldown(){
		return firingTimer >= firingRate;
	}



	
	protected virtual void Start () {
		//we want to be able to shoot when created
		firingTimer = firingRate;
	}


	// Update is called once per frame
	protected virtual void Update () {
		//adding by Time.deltaTime otherwise our firerate is bullets/frame instead of bullets/second
		if ( firingTimer <= firingRate) firingTimer += Time.deltaTime;
		
	}


	//shoots a bullet from object's position with an angle of dir
	public virtual void ShootDir ( Vector3 dir){
		if( FinishCooldown()){
			dir.Normalize();
			if(tag == "Enemy"){
				audio.clip = enemyShotSound;
				audio.PlayOneShot(enemyShotSound,1);
			}
			Vector3 temp = transform.position;
			temp.y += bulletHeight.y;
			clone = Instantiate(bullet, temp + dir * projectileStartPosition, Quaternion.LookRotation(dir, Vector3.down)) as Rigidbody;
			firingTimer = 0.0f;
			clone = Instantiate(shootEffect, temp + dir * 2, Quaternion.AngleAxis(-90, Vector3.forward)) as Rigidbody;
		}
	}

	//shoots a bullet from the object's position to the target point: targ
	public virtual void Shoot(Vector3 targ){
	
		ShootDir( targ - transform.position );
	}




}