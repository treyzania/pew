using UnityEngine;
using System.Collections;

public class LoadingMessage : MonoBehaviour {

	private const string PARAM_NAME = "ProcessingPurchase";
	public static LoadingMessage Active;
	
	private Animator anim;
	
	// Use this for initialization
	void Start () {
		
		this.anim = this.GetComponent<Animator>();
		Active = this;
		
	}
	
	public void SetActive(bool isActive) {
		if (this.anim != null) this.anim.SetBool(PARAM_NAME, isActive);
	}
	
	public bool IsActive() {
		
		if (this.anim != null) return this.anim.GetBool(PARAM_NAME);
		return false;
		
	}
	
}
