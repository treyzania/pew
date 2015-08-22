using UnityEngine;
using System.Collections;

public class MissileExploder : MonoBehaviour {

	public float BaseDamage = 0F;
	
	void OnTriggerEnter(Collider col) {
		
		HealthManager hm = col.gameObject.GetComponent<HealthManager>();
		
		//Debug.Log("I hit something!");
		
		if (hm != null) {
			
			//Debug.Log("Dealing damage!");
			hm.DealDamage(BaseDamage);
			
			GameObject.Destroy(this.gameObject);
			
		}
		
	}
	
}
