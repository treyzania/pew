using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Pew.Player;

public class MoneyDisplay : MonoBehaviour {

	public Text textElement;
	
	void Start () {
		
	}
	
	void Update () {
		
		textElement.text = StoredPlayerData.PLAYER_DATA.Money.ToString();
		
	}
	
}
