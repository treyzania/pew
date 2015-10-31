using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;
using Pew.Player;

public class AdDisplayer : MonoBehaviour {
	
	public const float E = 2.71828F; // Close enough.
	
	public string NextScene;
	
	// Logistic parameters.
	[Range(0, 1)] public float MaximumValue = 0.5F;
	[Range(0, 300)] public float SigmoidPoint = 50F;
	public float Steepness = 1F;
	
	public bool AlwaysShow = false;
	
	void Start () {
		
		// If you steal this code, I'll get the money from it anyways.
		//Advertisement.Initialize("1008015", false);
		
	}
	
	public void OnButton() {
		
		/*
		if (this.ShouldShowAdvertisement()) {
			Debug.Log("Showing advertisement...");
			this.ShowAdWhenReady();
		}
		*/
		
		Application.LoadLevel(this.NextScene);
		
	}
	
	private bool ShouldShowAdvertisement() {
		
		float duration = 30F; // Temporary value.
		if (GameTracker.Active != null) duration = float.Parse(GameTracker.Active.GetValue("time"));
		
		// Logistic function!
		float prob = this.MaximumValue / (1 + Mathf.Pow(E, -1 * this.Steepness * (duration - this.SigmoidPoint)));
		
		return (Random.Range(0, 1) <=  prob) || this.AlwaysShow;
		
	}
	
	private void ShowAdWhenReady() {
		
		while (!Advertisement.IsReady());
		Advertisement.Show();
		
	}
	
}
