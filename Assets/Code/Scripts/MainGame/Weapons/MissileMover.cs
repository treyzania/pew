using UnityEngine;
using System.Collections;

public class MissileMover : MonoBehaviour {

	public Vector3 Velocity;
	
	void Update () {
		
		this.transform.position += Velocity * Time.deltaTime;
		
	}
	
}
