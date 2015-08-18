using UnityEngine;
using System.Collections;

public class MissileExploder : MonoBehaviour {

	public float BaseDamage = 0F;
	
	void OnColllisionEnter(Collision col) {
		
		HealthManager hm = col.gameObject.GetComponent<HealthManager>();
		
		if (hm != null) {
			
			hm.DealDamage(BaseDamage);
			
		}
		
	}
	
}
