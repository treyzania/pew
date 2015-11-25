using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Pew.Player;

public class CannonFiring : MonoBehaviour {

	public Transform[] StartPoints;
	public Image ReloadBar; // Not the best place for this, but it works.
	[Range(0, 5)] public float ReloadTightness = 1F;
	
	public GameObject Missile;
	public float InitialMissileVelocity = 5F;
	public int FiringCost;
	
	public float FireThreshold = 1F;
	private float timeSinceFiring = 0F;
	
	private AudioSource SoundEffect;
	
	void Start() {
		
		this.timeSinceFiring = float.MaxValue;
		this.SoundEffect = this.GetComponent<AudioSource>();
		
	}
	
	void Update() {
		
		// Ineeficient, lazy lerp.
		Vector2 oldBar = new Vector2(ReloadBar.fillAmount, 0);
		Vector2 finalBarValue = Vector2.Lerp(oldBar, new Vector2(1 - (timeSinceFiring / this.FireThreshold), 0), this.ReloadTightness * Time.deltaTime);
		ReloadBar.fillAmount = finalBarValue.x;
		
	}
	
	public void TryFire() {
		
		if (this.CanFire()) {
			
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
			
			this.TakeMoney(this.FiringCost);
			this.timeSinceFiring = 0F;
			
			this.SoundEffect.Play();
			
		}
		
	}
	
	private bool CanFire() {
		
		bool timeRight = timeSinceFiring >- FireThreshold;
		bool hasMissile = this.Missile != null;
		bool hasMoney = (StoredPlayerData.PLAYER_DATA.Money + float.Parse(GameTracker.Active.GetValue("money_awarded"))) >= this.FiringCost;
		
		return timeRight && hasMissile && hasMoney;
		
	}
	
	private bool TakeMoney(int qty) {
		
		// Get the values out.
		int stored = StoredPlayerData.PLAYER_DATA.Money;
		int onHand = int.Parse(GameTracker.Active.GetValue("money_awarded"));
		
		// Technically, this could yield either negative values of free missiles.  Probably the latter.
		
		if (onHand >= qty || stored >= qty) {
			
			// (onHand >= qty ? onHand : stored) -= qty; No nice things here...
			
			if (onHand >= qty) {
				onHand -= qty;
			} else {
				stored -= qty;
			}
			
			// Put them back now.
			StoredPlayerData.PLAYER_DATA.Money = stored;
			GameTracker.Active.PutValue("money_awarded", onHand.ToString());
			
			return true;
			
		}
		
		// We wouldn't get here if we failed.
		return false;
		
	}
	
	void FixedUpdate() {
		
		this.timeSinceFiring += Time.fixedDeltaTime;
		
	}
	
}
