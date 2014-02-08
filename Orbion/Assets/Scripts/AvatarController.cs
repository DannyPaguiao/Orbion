﻿using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class AvatarController : MonoBehaviour {

	public CanMove moveScript;
	public CanShootReload shootScript;
	public AbsorbBullet absorbScript;
	public AudioClip shotSound;
	public Rigidbody grenade_prefab;


	private Rigidbody clone;

	public Rigidbody normalBullet;
	public Rigidbody orbBullet;
	//public CanUseEquip equipScript;


	private float ScatterSpread = 45f; //max angle that the scatter shot spreads to


	//Input.mousePosition gives you a screen position, not world position of the map.
	//To get the world position, we create a vertical ray from the screen position,
	//raycast it and if it hits something (like our floor), we take the position of
	//the point we hit. Note that we could easily raycast onto an object above the
	//floor and as such the y value (height) of this function is volatile and generally
	//unreliable/irrelevant. The function allows you to set the y value of the retuned
	// vector3 directly.
	protected Vector3 GetMouseWorldPos(float yvalue = -Mathf.Infinity){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		//floor layer is currently at 10 update this if that changes
		int layerMask = 1 << 10; 

		//raycasting onto lightshards were making the angle at which we shoot wonky
		//so only raycast onto things with the floor layer
		Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);

		//[Don't delete] debug code for showing where our mouse position is parsing into. 
		//Debug.DrawRay (ray.origin, hit.point);
		Vector3 mousePos = hit.point;
		if ( yvalue > -Mathf.Infinity) mousePos.y = yvalue;
		return mousePos;
	}


	//Shoots a scatter shot of bullets around the center direction: dir
	//   going from dir - ScatterSpread/2 to dir + ScatterSpread/2.
	//Works even if we're just shooting 1 bullet.
	protected void Scattershot(Vector3 target){
		Vector3 dir = target - transform.position;

		//if we have no ammo the for loop below won't run and the player won't auto reload
		//so forcing it to happen here
		if( shootScript.currentAmmo <= 0)
			shootScript.Shoot(dir);		

		if( shootScript.FinishCooldown()){
			//Vector3 hitAngle = adjustedHit - transform.position;
			Vector3 leftBound = Quaternion.Euler( 0, -ScatterSpread/2, 0) * dir;
			int ScatterCount = TechManager.GetUpgradeLv( Tech.scatter) + 1;
			int NumBulletsToShoot = Mathf.Min(ScatterCount, shootScript.currentAmmo);
			for ( int i = 1; i <= NumBulletsToShoot; i++){
				float angleOffset = i * ( ScatterSpread / ( NumBulletsToShoot + 1));
				Vector3 BulDir = Quaternion.Euler( 0, angleOffset, 0) * leftBound ;
				shootScript.SetFiringTimer( 1.0f);
				shootScript.ShootDir( BulDir);
				audio.clip = shotSound;
				audio.PlayOneShot(shotSound,1);
			}

		}
		
	}

	protected void GrenadeShot(Vector3 target){
		Vector3 dir = target - transform.position;
		dir.Normalize();
		
		if( shootScript.FinishCooldown()){

			clone = Instantiate(grenade_prefab, transform.position + dir * 2, Quaternion.LookRotation(dir, Vector3.up)) as Rigidbody;
			shootScript.ResetFiringTimer();

			
		}
		
	}




	// Use this for initialization
	void Start () {

	}



	void FixedUpdate() {
		if( Input.GetKey( KeyCode.W)){

			moveScript.Move( Vector3.forward);
		}
		if( Input.GetKey( KeyCode.S)){

			moveScript.Move( Vector3.back);

		}
		if( Input.GetKey( KeyCode.D)){

			moveScript.Move( Vector3.right);

		}
		if( Input.GetKey( KeyCode.A)){

			moveScript.Move( Vector3.left);

		}
		if( Input.GetKeyDown( KeyCode.R)){

			shootScript.Reload();

		}
	
	}
	


	// Update is called once per frame
	void Update () {
		//[Don't delete] debug code for showing our shooting angle
		//Debug.DrawRay(transform.position, GetMouseWorldPos(transform.position.y) - transform.position);
		//Uses the CanShootReload component to shoot at the cursor
		if( Input.GetMouseButton( 0)){

			Scattershot( GetMouseWorldPos( transform.position.y));
		}
		if(Input.GetMouseButton(1)){
			//if (TechManager.HasUpgrade(Tech.light_grenade))
			//GrenadeShot(GetMouseWorldPos(transform.position.y));
				absorbScript.AbsorbShot(GetMouseWorldPos( transform.position.y));
		
		}



		/*if((Input.GetKey(KeyCode.A))||(Input.GetKey(KeyCode.W))||(Input.GetKey(KeyCode.S))||(Input.GetKey(KeyCode.D))){
			audio.clip = shotSound;
			audio.Play();
		}
		else
			audio.Pause();
		*/

	}
}
