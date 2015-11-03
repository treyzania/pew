using UnityEngine;
using System.Collections;

public class MissileAoe : MonoBehaviour {
	
	public float BaseDamage = 0F;
	[Range(0, 10)] public float MaxRange = 1F;
	[Range(0.2F, 5)] public float RadiusExponent = 1F;
	public float MaxDamage = 100F;
	
	public string TargetTag;
	
	public GameObject SpawnAfter;
	public float SAScale = 1F;
	
	void OnTriggerEnter(Collider hit) {
		
		Collider[] cols = Physics.OverlapSphere(this.transform.position, this.MaxRange);
		
		if (hit.gameObject.CompareTag(this.TargetTag)) {
			
			foreach (Collider col in cols) {
				
				HealthManager hm = col.gameObject.GetComponent<HealthManager>();
				
				if (hm != null && hm.gameObject.CompareTag(TargetTag)) {
					
					float distanceSq = (col.transform.position - this.transform.position).sqrMagnitude;
					float damageDone = BaseDamage * Mathf.Pow(distanceSq, this.RadiusExponent / -2F);
					
					/*
					 * -2 because we want it in the denominator, and it's already being squared.
					 */
					
					Debug.Log("Ka bang.");
					
					hm.DealDamage(Mathf.Min(damageDone, MaxDamage));
					
				}
				
			}
			
			GameObject.Destroy(this.gameObject);
			
			GameObject after = (GameObject) GameObject.Instantiate(this.SpawnAfter, this.transform.position, Quaternion.identity);
			after.transform.localScale = Vector3.one * this.SAScale;
			
		}
		
	}
	
}
