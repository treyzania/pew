using UnityEngine;
using System.Collections;

public class CannonTargeting : MonoBehaviour {

	public RectTransform Target;
	public Transform ShipRoot;
	public float RotationAdjustFactor = 10F;
	//public bool LockX, LockY, LockZ;
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// Calculate the vector.
		Vector3 adjPos = this.Target.position - Camera.current.WorldToScreenPoint(this.ShipRoot.position);
		Vector3 rotatedAdj = new Vector3(-adjPos.x, 0, -adjPos.y);
		
		// Set the direction.
		this.transform.rotation = Quaternion.Slerp(
			this.transform.rotation,
			Quaternion.LookRotation(rotatedAdj, Vector3.up),
			Time.deltaTime * this.RotationAdjustFactor
		);
		
	}
	
}
