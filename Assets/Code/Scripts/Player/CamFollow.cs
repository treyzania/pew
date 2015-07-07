using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

	public Transform Following = null;
	public float Tightness = 1F;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		//float distanceSq = (this.transform.position - this.Following.position).sqrMagnitude;
		
		this.transform.position = Vector3.Lerp(this.transform.position, this.Following.position, Time.deltaTime * this.Tightness);
		
	}
	
}
