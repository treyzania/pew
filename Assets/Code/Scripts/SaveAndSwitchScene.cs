using UnityEngine;
using System.Collections;
using Pew.Google;

public class SaveAndSwitchScene : MonoBehaviour {

	public string NextScene;
	public bool DoLoadingMessage = false;
	public bool CancelPurchasing = false;
	
	private bool saveInProgress = false;
	
	public void OnButton() {
		
		Debug.Log("Button to save then load " + this.NextScene + " pushed.");
		
		if (saveInProgress) {
			Debug.Log("Aborting!  Save in progress!");
			return;
		}
		
		saveInProgress = true;
		
		if (this.CancelPurchasing) ButtonBuy.AllowPurchasing = false;
		if (this.DoLoadingMessage && LoadingMessage.Active != null) LoadingMessage.Active.SetActive(true);
		
		GoogleFrontend.Save();
		Application.LoadLevelAsync(this.NextScene);
		
	}
	
}
