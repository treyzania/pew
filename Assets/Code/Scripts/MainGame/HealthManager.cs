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
			
			PointValue points = this.GetComponent<PointValue>();
			if (points != null) points.AwardPoints();
			
			GameObject.Destroy(this.gameObject);
			
			// TODO Modularize.
			
			PlayerDeath pd = this.GetComponent<PlayerDeath>();
			if (pd != null) pd.HandleDeath();
			
			EnemyDeath ed = this.GetComponent<EnemyDeath>();
			if (ed != null) ed.HandleDeath();
			
		}
		
	}
	
}
