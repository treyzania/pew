using UnityEngine;
using System.Collections;

public class TurnTowardsTravel : MonoBehaviour {

	public float RotationSpeed = 1F;
	
	private Rigidbody rb;
	
	void Start() {
		this.rb = this.GetComponent<Rigidbody>();
	}
	
	void Update() {
		
		Quaternion currentRot = this.transform.rotation;
		Quaternion travelRot = Quaternion.LookRotation(this.rb.velocity * -1);
		
		this.transform.rotation = Quaternion.Slerp(currentRot, travelRot, RotationSpeed * Time.deltaTime);
		
	}
	
}
