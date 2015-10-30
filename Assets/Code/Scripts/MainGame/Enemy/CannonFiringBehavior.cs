using UnityEngine;
using System.Collections;
using Pew.Player;

public class LaserFiringBehavior : MonoBehaviour {

	public GameObject MisslePrefab = null;
	public Transform[] BarrelEnds = null;
	public float FiringVelocity = 1F;
	
	public float FiringRate = 1F;
	public float AttackRadius = 5F;
	
	private float Cooldown = 0F;
	
	void Start() {
		
	}
	
	void Update() {
		
		if (Ship.PlayerInstance.Container == null) return; // Oops.
		
		Vector3 delta = Ship.PlayerInstance.Container.gameObject.transform.position - this.transform.position;
		
		if (delta.sqrMagnitude <= Mathf.Pow(AttackRadius, 2)) {
			
			Cooldown -= Time.deltaTime;
			
			if (Cooldown <= 0) {
				
				Cooldown = FiringRate;
				
				Debug.Log("Shooting!");
				
				foreach (Transform t in BarrelEnds) {
					
					GameObject missile = GameObject.Instantiate(MisslePrefab);
					
					missile.transform.position = t.transform.position;
					missile.transform.rotation = t.transform.rotation;
					
					MissileMover mm = missile.GetComponent<MissileMover>();
					MissileAoe ma = missile.GetComponent<MissileExploder>();
					
					if (mm != null) mm.Velocity = delta.normalized * FiringVelocity;
					if (ma != null) me.TargetTag = "Player";
					
				}
				
			}
			
		} else {
			Cooldown = 0f; // Set it always, faster than checking.
		}
		
	}
	
}
