using UnityEngine;
using System.Collections;
using Pew.Player;

public class CannonFiring : MonoBehaviour {

	public Transform[] StartPoints;
	
	public GameObject Missile;
	public float InitialMissileVelocity = 5F;
	public int FiringCost;
	
	public float FireThreshold = 1F;
	private float timeSinceFiring = 0F;
	
	void Start() {
		
		this.timeSinceFiring = float.MaxValue;
		
	}
	
	public void TryFire() {
		
		if (timeSinceFiring >= FireThreshold && this.Missile != null && StoredPlayerData.PLAYER_DATA.Money >= this.FiringCost) {
			
			foreach (Transform t in this.StartPoints) {
				
				GameObject m = GameObject.Instantiate(this.Missile);
				
				m.transform.position = t.position;
				m.transform.rotation = t.rotation;
				
				MissileMover mm = m.GetComponent<MissileMover>();
				MissileAoe ma = m.GetComponent<MissileAoe>();
				
				if (ma != null) ma.TargetTag = "Enemy";
				
				// Up because of how the rotation is.
				if (mm != null) mm.Velocity = m.transform.up * InitialMissileVelocity;
				
			}
			
			StoredPlayerData.PLAYER_DATA.Money -= this.FiringCost;
			this.timeSinceFiring = 0F;
			
		}
		
	}
	
	void FixedUpdate() {
		
		this.timeSinceFiring += Time.fixedDeltaTime;
		
	}
	
}
