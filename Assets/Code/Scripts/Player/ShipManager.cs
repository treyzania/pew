using UnityEngine;
using System.Collections;

public class ShipManager : MonoBehaviour {

	public float CurrentThrottle = 0F;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public float GetMass() {
		// FIXME Use from hull.
		return 1000F;
	}
	
	public float GetCurrentEngineMaxThrust() {
		// FIXME Base off of energy availability, engine max thrust, and engine efficiency.
		return 100F;
	}
	
}
