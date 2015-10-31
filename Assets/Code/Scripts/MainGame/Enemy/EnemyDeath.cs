using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour {

	public GameObject DeathEffect;
	public float Scaling = 1F;
	
	public void HandleDeath() {
		
		GameObject posteffect = (GameObject) GameObject.Instantiate(this.DeathEffect, this.transform.position, Quaternion.identity);
		posteffect.transform.localScale = Vector3.one * this.Scaling;
		
	}
	
}
