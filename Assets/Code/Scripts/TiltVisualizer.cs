using UnityEngine;
using System.Collections;

public class TiltVisualizer : MonoBehaviour {
	
	public float Scale = 1F;
	public float Tightness = 1F;
	
	public Transform Model = null;
	
	void Update() {
		
		this.Model.localPosition = Vector3.Lerp(this.Model.localPosition, Input.acceleration, Tightness * Time.deltaTime);
		
	}
	
}
