﻿using UnityEngine;
using System.Collections;

public class CannonFiring : MonoBehaviour {

	public Transform[] StartPoints;
	
	public GameObject Missile;
	public float InitialMissileVelocity = 5F;
	public int MissleCost;
	
	public float FireThreshold = 1F;
	private float timeSinceFiring = 0F;
	
	public void TryFire() {
		
		if (timeSinceFiring >= FireThreshold && this.Missile != null) {
			
			foreach (Transform t in this.StartPoints) {
				
				GameObject m = GameObject.Instantiate(this.Missile);
				
				m.transform.position = t.position;
				m.transform.rotation = t.rotation;
				
				MissileMover mm = m.GetComponent<MissileMover>();
				
				// Up because of how the rotation is.
				if (mm != null) mm.Velocity = m.transform.up * InitialMissileVelocity;
				
			}
			
			this.timeSinceFiring = 0F;
			
		}
		
	}
	
	void FixedUpdate() {
		
		this.timeSinceFiring += Time.fixedDeltaTime;
		
	}
	
}
