using UnityEngine;
using System.Collections;

public class CannonFiring : MonoBehaviour {

	public Transform BarrelEnd = null;
	public GameObject Missile = null;
	public float InitialMissileVelocity = 5F;
	[Range(0, 10)] public float FireThreshold = 1F;
	
	private float timeSinceFiring = 0F;
	
	public void TryFire() {
		
		Debug.Log("Firing cannon. " + timeSinceFiring + ", " + Time.fixedDeltaTime);
		
		if (timeSinceFiring <= FireThreshold) {
			
			if (Missile != null) {
				
				GameObject m = GameObject.Instantiate(this.Missile);
				
				m.transform.position = this.BarrelEnd.position;
				m.transform.rotation = this.BarrelEnd.rotation;
				
				Rigidbody rb = m.GetComponent<Rigidbody>();
				
				// Up because of how the rotation is.
				rb.velocity = m.transform.up * InitialMissileVelocity;
				
			}
			
		}
		
		this.timeSinceFiring = 0F;
		
	}
	
	void Update() {
		
		if (Input.touches.Length > 0) {
			
			if (this.timeSinceFiring >= this.FireThreshold) {
				this.TryFire();
				this.timeSinceFiring = 0F;
			}
			
		}
		
	}
	
	void FixedUpdate() {
		
		this.timeSinceFiring += Time.fixedDeltaTime;
		
	}
	
}
