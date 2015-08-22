using UnityEngine;
using System.Collections;

public class KeepAbsoluteOrientation : MonoBehaviour {

	private Quaternion initRot;
	
	void Start() {
		this.initRot = this.transform.rotation;
	}
	
	void FixedUpdate() {
		this.transform.rotation = this.initRot;
	}
	
}
