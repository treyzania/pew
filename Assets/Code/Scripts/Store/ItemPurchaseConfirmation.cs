using UnityEngine;
using System;
using System.Collections;

public class ItemPurchaseConfirmation : MonoBehaviour {

	private const string ANIM_DISPLAYING_KEY = "Displaying";
	private const string ANIM_CHOICE_KEY = "Choice";
	
	public static ItemPurchaseConfirmation Active;
	
	private Animator Anim;
	private ButtonBuy ButtonInQuestion;
	private Action<bool> Callback;
	
	void Start () {
		
		Active = this;
		
		this.Anim = this.GetComponent<Animator>();
		
	}
	
	public void ResultYes() {
		
		this.Anim.SetInteger(ANIM_CHOICE_KEY, 1);
		this.Callback.Invoke(true);
		this.Cleanup();
		
	}
	
	public void ReusltNo() {
		
		this.Anim.SetInteger(ANIM_CHOICE_KEY, -1);
		this.Callback.Invoke(false);
		this.Cleanup();
		
	}
	
	private void Cleanup() {
		
		this.Anim.SetBool(ANIM_DISPLAYING_KEY, false);
		
	}
	
	public void BeginPurchase(ButtonBuy buy, Action<bool> result) {
		
		this.Anim.SetBool(ANIM_DISPLAYING_KEY, true);
		
		this.ButtonInQuestion = buy;
		this.Callback = result;
		
	}
	
}
