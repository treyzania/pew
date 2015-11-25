using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Pew.Player;

public class MoneyDisplay : MonoBehaviour {

	public Text textElement;
	
	void Update () {
		
		textElement.text = (StoredPlayerData.PLAYER_DATA.Money + float.Parse(GameTracker.Active.GetValue("money_awarded"))).ToString();
		
	}
	
}
