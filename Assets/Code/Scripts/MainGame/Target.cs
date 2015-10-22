using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	public RectTransform Master;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		Ray ray = Camera.main.ScreenPointToRay(Master.position);
		
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {
			
			this.transform.position = hit.point;
			
		}
		
	}
}
