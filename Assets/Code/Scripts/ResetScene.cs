using UnityEngine;
using System.Collections;

public class ResetScene : MonoBehaviour {
	
	void Start() {
		
		// Blah.
		
	}
	
	public void DoReset() {
		
		Application.LoadLevel(Application.loadedLevel);
		
	}
	
}
