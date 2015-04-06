using UnityEngine;
using System.Collections;

public class GyroMove : MonoBehaviour {

	public float VelocityFactor = 5F;
	public float RotationFactor = 5F;
	public float RotationAdjustCutoff = 0.05F;
	
	private Quaternion lastFacingTarget;
	
	void Start () {
		
		this.lastFacingTarget = Quaternion.identity;
		
	}
	
	void Update () {
		
		Vector3 acceleration = new Vector3(Input.acceleration.x, 0, Input.acceleration.y);
		if (acceleration.magnitude > this.RotationAdjustCutoff) this.lastFacingTarget = Quaternion.LookRotation(acceleration);
		
		this.GetComponent<Rigidbody>().AddForce(acceleration * this.VelocityFactor);
		this.transform.rotation = Quaternion.Slerp(
			this.transform.rotation,
			this.lastFacingTarget,
			Time.deltaTime * this.RotationFactor
		);
		
	}
	
}
