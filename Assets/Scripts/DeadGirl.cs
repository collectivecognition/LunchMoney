using UnityEngine;
using System.Collections;

public class DeadGirl : MonoBehaviour {

	private float blinkDelay = 0.1f;
	private float blinkTime = 0f;
	private int blinkTimes = 8;
	private int blinkCount = 0;

	// Use this for initialization
	void Start () {
		blinkTime = Time.time + 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > blinkTime + blinkDelay){
			GetComponent<Renderer>().enabled = false;
		}
		
		if(Time.time > blinkTime + blinkDelay * 2){
			GetComponent<Renderer>().enabled = true;
			blinkCount++;
			blinkTime = Time.time;
		}
		
		if(blinkCount > blinkTimes){
			Application.LoadLevel ("IntroScene");
		}
	}
}
