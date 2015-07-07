using UnityEngine;
using System.Collections;

public class MissileDecay : MonoBehaviour {

	public float MaxRange = 10F;
	private Vector3 StartPos = Vector3.zero;
	
	// Use this for initialization
	void Start () {
		this.StartPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		if ((this.transform.position - this.StartPos).sqrMagnitude >= Mathf.Pow(MaxRange, 2)) {
			GameObject.Destroy(this.gameObject);
		}
		
	}
}
