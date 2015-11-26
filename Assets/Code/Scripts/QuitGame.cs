using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {

	public GameObject SpawnAtPlayer;
	
	public void QuitTheGame() {
		
		GameObject player = GameObject.Find("Player");
		
		GameObject.Instantiate(this.SpawnAtPlayer, player.transform.position, Quaternion.identity);
		GameObject.Destroy(player);
		player.GetComponent<PlayerDeath>().HandleDeath();
		
	}
	
}
