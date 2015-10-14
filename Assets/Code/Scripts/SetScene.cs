using UnityEngine;
using System.Collections;

public class SetScene : MonoBehaviour {
	
	public string LevelName;
	
	public void DoChange() {
		
		Application.LoadLevel(this.LevelName);
		
	}
	
}
