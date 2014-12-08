using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PressSpace : MonoBehaviour {
	public Image pressSpace;

	void Start(){
		// iTween.PunchScale (pressSpace, iTween.Hash ("looptype", iTween.LoopType.pingPong, "time", 1f, "amount", 1.1f));
	}

	void Update(){
		if(Input.GetKeyDown (KeyCode.Space)){
			Debug.Log ("Space pressed");
			Application.LoadLevel ("MainScene");
		}
	}
}
