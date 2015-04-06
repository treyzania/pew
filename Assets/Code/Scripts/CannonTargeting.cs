using UnityEngine;
using System.Collections;

public class CannonTargeting : MonoBehaviour {

	public RectTransform Target;
	public float RotationAdjustFactor = 10F;
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 orig = this.Target.position;
		
		Vector3 adjPos = Vector3.zero;
		
		this.transform.rotation = Quaternion.Slerp(
			this.transform.rotation,
			Quaternion.LookRotation(adjPos, Vector3.up),
			Time.deltaTime * this.RotationAdjustFactor
		);
		
	}
	
}
