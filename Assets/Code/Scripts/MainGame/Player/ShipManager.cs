using UnityEngine;
using System.Collections;
using Pew.Player;

// TODO Remove this class.

public class ShipManager : MonoBehaviour {

	public Ship TheShip;
	
	// Use this for initialization
	void Start () {
		
		this.TheShip = new Ship();
		this.TheShip.Container = this.gameObject;
		
		Ship.PlayerInstance = this.TheShip;
		
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
