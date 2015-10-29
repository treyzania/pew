using UnityEngine;
using System.Collections;

public class MissileAoe : MonoBehaviour {
	
	public float BaseDamage = 0F;
	[Range(0, 10)] public float MaxRange = 1F;
	[Range(0.2, 5)] public float RadiusExponent = 1F;
	
	public string TargetTag;
	
	void OnTriggerEnter(Collider ignored) {
		
		Collider[] cols = Physics.OverlapSphere(this.transform.position, this.MaxRange);
		
		foreach (Collider col in cols) {
			
			HealthManager hm = col.gameObject.GetComponent<HealthManager>();
			
			if (hm != null && hm.gameObject.CompareTag(TargetTag)) {
				
				float distanceSq = (col.transform.position - this.transform.position).sqrMagnitude;
				float damageDone = BaseDamage * Mathf.Pow(distanceSq, this.RadiusExponent / -2F);
				
				/*
				 * -2 because we want it in the denominator, and it's already being squared.
				 */
				
				hm.DealDamage(damageDone);
				GameObject.Destroy(this.gameObject);
				
			}
			
		}
		
	}
	
}
