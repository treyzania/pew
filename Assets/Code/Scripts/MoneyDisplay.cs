using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Pew.Player;

public class MoneyDisplay : MonoBehaviour {

	public Text textElement;
	
	void Update () {
		
		int total = StoredPlayerData.PLAYER_DATA.Money;
		if (GameTracker.Active != null) total += (int) float.Parse(GameTracker.Active.GetValue("money_awarded"));
		
		textElement.text = total.ToString();
		
	}
	
}
