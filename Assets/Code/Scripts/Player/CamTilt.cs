using UnityEngine;
using System.Collections;

public class CamTilt : MonoBehaviour {

	public float ChangeThreshold = 0.25F;
	public float UpwardsBias = 1F;
	public float TiltTightness = 5F;
	public float RotationTightness = 1F;
	
	private Vector3 effectiveTilt;
	private Quaternion targetRot;
	
	void Start () {
		
		this.effectiveTilt = Vector3.down;
		this.targetRot = Quaternion.identity;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// Inits.
		Vector3 tilt = Input.acceleration;
		this.effectiveTilt = Vector3.Slerp(this.effectiveTilt, tilt, TiltTightness * Time.deltaTime); // Update the tiltiness.
		
		if (effectiveTilt.sqrMagnitude >= Mathf.Pow(ChangeThreshold, 2)) {
			
			targetRot = Quaternion.LookRotation(
				
				// XXX Scary!
				new Vector3(
					this.effectiveTilt.x,
					this.effectiveTilt.y,
					UpwardsBias
				),
					
					Vector3.up
					
				)
			;
			
		}
		
		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.targetRot, RotationTightness * Time.deltaTime);
		
	}
	
}
