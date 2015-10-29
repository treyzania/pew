using UnityEngine;
using System.Collections;

public class LaserFiring : MonoBehaviour {

	public Transform BarrelEnd = null;
	public GameObject Missile = null;
	public float InitialMissileVelocity = 5F;
	public float FireThreshold = 1F;
	
	private float timeSinceFiring = 0F;
	
	public float DamageDealt;
	
	void Start() {
		
		// HULLO!
		
	}
	
	public void TryFire() {
		
		//Debug.Log("Firing cannon. " + timeSinceFiring + ", " + Time.fixedDeltaTime);
		
		if (timeSinceFiring >= FireThreshold) {
			
			if (this.Missile != null) {
				
				GameObject m = GameObject.Instantiate(this.Missile);
				
				m.transform.position = this.BarrelEnd.position;
				m.transform.rotation = this.BarrelEnd.rotation;
				
				MissileMover mm = m.GetComponent<MissileMover>();
				MissileExploder me = m.GetComponent<MissileExploder>();
				
				// Up because of how the rotation is.
				mm.Velocity = m.transform.up * InitialMissileVelocity;
				
				me.TargetTag = "Enemy";
				me.BaseDamage = this.DamageDealt;
				
				//Debug.Log(m.transform.position);
				
			}
			
			this.timeSinceFiring = 0F;
			
		}
		
	}
	
	void Update() {
		
		if (Input.touches.Length > 0) this.TryFire();
		
	}
	
	void FixedUpdate() {
		
		this.timeSinceFiring += Time.fixedDeltaTime;
		
	}
	
}
