﻿using UnityEngine;
using System.Collections;

//This a simple behaviour which moves the bullet in a straight line
//with constant force/impulse over time.
//It will apply a single damage to whatever it collides and kill itself.

[RequireComponent (typeof (ProjectileController))]
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Collider))]
[RequireComponent (typeof (CanMove))]

public class PB_Linear : ProjectileBehavior {

	public ForceMode MoveType;
	//public float MoveForce;
	public CanMove MoveScript;

	public AudioClip buildinghitSound;
	public AudioClip enemyhitSound;


	public int Damage;
	public int searingLevel;
	//public int homingLevel;
	//public int ricochetLevel;
	private int health = 1;
	public GameObject dot;
	public GameObject hitEffect;
	private GameObject clone;

	private GameObject lastHitTarget;

	public GameObject target;

	public float lifeTime = 2.0F;
	private float lifeCounter = 0.0F;

	//gap in time before we can find another target to home into
	private const float seekerFindTargetRate = 0.25f;
	private GameObject seekerTarget;

	
	public IEnumerator FindSeekerTarget(){
		while( true){
			if(seekerTarget == null && TechManager.GetNumBuilding(Tech.incendiary) > 0)
				seekerTarget = Utility.GetClosestWith(transform.position, 15*TechManager.GetUpgradeLv(Tech.seeker), IsTarget, Utility.Enemy_PLM);
			yield return new WaitForSeconds(seekerFindTargetRate);
		}
	}


	public override void Initialize(){
		StartCoroutine( FindSeekerTarget());
		health = TechManager.GetUpgradeLv (Tech.ricochet);
	}

	public void Update(){
		if(lifeCounter > lifeTime){
			Destroy(this.gameObject);
		} else {
			lifeCounter += Time.deltaTime;
		}
	}

	public override void FixedPerform(){
		MoveScript.Move(transform.forward, MoveType);

		if( seekerTarget != null){
			Vector3 targDir = transform.InverseTransformPoint(seekerTarget.transform.position);
			float targAngle = Mathf.Atan2(targDir.x, targDir.z);
			
			//Debug.Log(targAngle);
			
			if(targAngle < 0 && targAngle > -1){
				MoveScript.TurnLeft(Vector3.up, MoveType);
			}else if(targAngle > 0 && targAngle < 1){
				MoveScript.TurnRight(Vector3.up, MoveType);
			}
			
			//Vector3 newDir = Vector3.RotateTowards(transform.forward, targDir, 0.3f, 0.0f);
			//transform.rotation = Quaternion.LookRotation(newDir);
		}

	}

	public override void Perform(){ return;}


	public bool IsTarget(GameObject enemy){
		if(enemy.GetComponent<IsEnemy>() == null && enemy.GetComponent<Buildable>() == null) return false;
		if (enemy == lastHitTarget)
						return false;

		return true;
	}
	

	public override void OnImpactEnter( Collision other){
		Killable KillScript = other.gameObject.GetComponent<Killable>();
		if( KillScript) {
			if(other.gameObject.GetComponent<IsEnemy>() != null){
				audio.PlayOneShot(enemyhitSound);
			}

			if(other.gameObject.GetComponent<Buildable>() != null && this.tag == "playerBullet"){
				audio.PlayOneShot(buildinghitSound);
				if(KillScript.currHP < KillScript.baseHP){

					KillScript.Heal(Damage);
					if(other.gameObject.GetComponent<IsDamagedEffect>() != null){
						other.gameObject.GetComponent<IsDamagedEffect>().removeDamage();
					}
				}else{
					//Physics.IgnoreCollision(gameObject.collider, other.collider);
					//return;
				}
			}
			else{
				KillScript.damage(Damage);
				clone = Instantiate(hitEffect, transform.position, new Quaternion()) as GameObject;
			}
		}

		//drop a DOT on target if searing is > 0
		if(searingLevel > 0 && other.gameObject.tag == "Enemy"){
			clone = Instantiate(dot, other.transform.position, new Quaternion()) as GameObject;
			clone.GetComponent<IsSearingShot>().target = other.gameObject.GetComponent<Rigidbody>();
		}



		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyRanged") {
			target = GameObject.Find("player_prefab");
			target.GetComponent<hasOverdrive>().overdriveCount += 1.0f;
			//ebug.Log(target.GetComponent<hasOverdrive>().overdriveCount);
		}
		if (TechManager.GetUpgradeLv(Tech.ricochet) > 0 && TechManager.GetNumBuilding(Tech.photon) > 0 && health > 0) {
			health--;
			Physics.IgnoreCollision(gameObject.collider, other.collider);
			if(lastHitTarget != null)
				Physics.IgnoreCollision(gameObject.collider, lastHitTarget.collider, false);
			lastHitTarget = other.gameObject;
			gameObject.rigidbody.velocity = Vector3.zero;
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			GameObject targ = Utility.GetClosestWith(transform.position, 15*TechManager.GetUpgradeLv(Tech.ricochet), IsTarget);

	

			if(targ == null){
				foreach( Transform child in transform){
					if(child.gameObject.tag == "playerBullet"){
						Destroy(child.gameObject);
					}
					else{
						ParticleSystem childParticle = child.gameObject.GetComponent<ParticleSystem>(); 
						if( childParticle){
							childParticle.loop = false;
							childParticle.transform.parent = null;
						}
					}
				}
				Destroy (gameObject);
			}else{

				Vector3 targDir =  targ.transform.position - transform.position;
			
				transform.rotation = Quaternion.LookRotation(targDir, Vector3.up);
			}

		} else {
			foreach( Transform child in transform){
				if(child.gameObject.tag == "playerBullet"){
					Destroy(child.gameObject);
				}
				else{
					ParticleSystem childParticle = child.gameObject.GetComponent<ParticleSystem>(); 
					if( childParticle){
						childParticle.loop = false;
						childParticle.transform.parent = null;
					}
				}
			}
			Destroy (gameObject);
		}
	}

	public override void OnImpactStay( Collision other){ return;}

	public override void OnImpactExit( Collision other){ return;}

}
