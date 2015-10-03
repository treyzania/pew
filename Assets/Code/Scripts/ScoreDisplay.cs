using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {
	
	public int Score = 0;
	
	private Text textElement;
	
	void Start() {
		
		this.textElement = this.gameObject.GetComponentsInChildren<Text>()[1];
		
	}
	
	void Update() {
		
		this.textElement.text = Convert.ToString(this.Score);
		
	}
	
	public void AddPoints(int numb) {
		
		Score += numb;
		
	}
	
}
