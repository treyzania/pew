using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthMeter : MonoBehaviour {

	public Image ImageComponent;
	
	public float MaxHealth = 100F;
	public float CurrentHealth = 100F;
	
	private HealthManager hm;
	
	void Start() {
		
		this.hm = this.GetComponent<HealthManager>();
		
	}
	
	void Update () {
		
		if (this.hm != null) {
			
			this.MaxHealth = hm.MaxHealth;
			this.CurrentHealth = hm.Health;
			
		}
		
		this.ImageComponent.fillAmount = Mathf.Clamp01(this.CurrentHealth / this.MaxHealth);
		
	}
}
