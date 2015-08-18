using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour {

	public float Health, MaxHealth;
	public float DefenseFactor;
	
	void Start () {
		
	}
	
	void Update () {
		
	}
	
	public void DealDamage(float damage) {
		
		Health -= damage * DefenseFactor; // Apply damage.
		
		this.HandleDeath();
		
	}
	
	public void HandleDeath() {
		
		if (Health <= 0) {
			
			Debug.Log("I died!");
			
			GameObject.Destroy(this.gameObject);
			
		} else {
			Debug.Log("Not dead!");
		}
		
	}
	
}
