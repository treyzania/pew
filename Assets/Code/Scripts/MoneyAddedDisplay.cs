using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Pew.Player;
using Pew.Google;

public class MoneyAddedDisplay : MonoBehaviour {
	
	public Text value;
	public string Prefix = "+ ";
	
	void Start() {
		
		// TODO Achievement checking.
		
	}
	
	void Update () {
		
		if (GameTracker.Active != null) {
			value.text = this.Prefix + GameTracker.Active.GetValue("money_awarded");
		} else {
			value.text = "?";
		}
		
	}
	
	void OnDestroy() {
		
		if (GameTracker.Active != null) {
			
			StoredPlayerData.PLAYER_DATA.Money += (int) float.Parse(GameTracker.Active.GetValue("money_awarded"));
			GoogleFrontend.Save();
			
		}
		
	}
	
}
