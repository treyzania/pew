﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShipManager))]
public class GyroMove : MonoBehaviour {

	[Range(0, 10)] public float VelocityFactor = 5F;
	[Range(0, 2.5F)] public float VelocityExponent = 1F;
	[Range(0, 50)] public float RotationFactor = 1F;
	[Range(0, 25F)] public float MaxVelocity = 10F;
	
	[Range(0, 1)] public float RotationAdjustCutoff = 0.1F;
	[Range(0, 1)] public float DeadzoneCutoff = 0.05F;
	
	private ShipManager sm;
	private Quaternion lastFacingTarget;
	
	void Start () {
		
		this.sm = this.GetComponent<ShipManager>();
		
		this.lastFacingTarget = Quaternion.identity;
		
	}
	
	void Update () {
		
		Vector3 dir = new Vector3(Input.acceleration.x, 0, Input.acceleration.y);
		Vector3 norm = dir.normalized;
		
		// Calculate the magnitude.
		float inertiaFactor = sm.GetCurrentEngineMaxThrust() / sm.GetMass();
		float throttleFactor = Mathf.Pow(dir.magnitude, VelocityExponent);
		float mag = Mathf.Min(inertiaFactor * VelocityFactor * throttleFactor, this.MaxVelocity);
		
		// Update the position.
		if (dir.magnitude > DeadzoneCutoff) this.transform.position += norm * mag;
		
		// Update the rotation.
		if (mag > this.RotationAdjustCutoff) {
			
			this.lastFacingTarget = Quaternion.LookRotation(norm, Vector3.up);
			this.transform.rotation = Quaternion.Slerp(
				this.transform.rotation,
				this.lastFacingTarget,
				Time.deltaTime * RotationFactor
			);
			
		}
		
	}
	
}
