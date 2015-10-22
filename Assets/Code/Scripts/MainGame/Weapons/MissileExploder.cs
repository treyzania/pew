using UnityEngine;
using System.Collections;

public class MissileExploder : MonoBehaviour {

	public float BaseDamage = 0F;
	public string TargetTag;
	
	void OnTriggerEnter(Collider col) {
		
		HealthManager hm = col.gameObject.GetComponent<HealthManager>();
		
		if (hm != null) {
			
			Debug.Log("Ka boom.");
			
			if (hm.gameObject.CompareTag(TargetTag)) {
				
				hm.DealDamage(BaseDamage);
				GameObject.Destroy(this.gameObject);
				
			}
			
		}
		
	}
	
}
