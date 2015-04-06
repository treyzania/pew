using UnityEngine;
using System.Collections;

public class GyroOrientation : MonoBehaviour {

	public float MovementFactor = 1F;
	
	// Use this for initialization
	void Start () {
		
		Input.gyro.enabled = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		this.transform.rotation = Quaternion.LookRotation(Input.gyro.gravity * -1);
		
	}
}
