using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthMeter : MonoBehaviour {

	public Image ImageComponent;
	
	public float MaxHealth = 100F;
	public float CurrentHealth = 100F;
	
	void Update () {
		
		this.ImageComponent.fillAmount = Mathf.Clamp01(this.CurrentHealth / this.MaxHealth);
		
	}
}
