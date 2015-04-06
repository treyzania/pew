using UnityEngine;
using System.Collections;

public class FollowTouch : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		Touch[] ts = Input.touches;
		
		if (ts.Length > 0) {
			
			Debug.Log(ts[0].position.x + ", " + ts[0].position.y);
			this.transform.position = new Vector3(ts[0].position.x, ts[0].position.y, 0);
			
		}
		
	}
}
