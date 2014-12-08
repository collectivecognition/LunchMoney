using UnityEngine;
using System.Collections;

public class Turkey : MonoBehaviour {
	
	private float bobAmount = 0.1f;
	private float bobDuration = 0.05f;
	
	// Use this for initialization
	void Start () {
		iTween.ScaleAdd(gameObject,iTween.Hash("y", bobAmount, "time", bobDuration, "looptype", iTween.LoopType.pingPong, "easing", iTween.EaseType.easeInCubic, "delay", Random.Range (0f, 0.3f)));
	}
}
