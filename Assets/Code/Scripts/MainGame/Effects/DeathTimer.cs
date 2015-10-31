using UnityEngine;
using System.Collections;

public class DeathTimer : MonoBehaviour {

	public float TimeAlive = 1F;
	
	void FixedUpdate () {
		
		this.TimeAlive -= Time.fixedDeltaTime;
		
		if (this.TimeAlive <= 0) GameObject.Destroy(this.gameObject);
		
	}
	
}
