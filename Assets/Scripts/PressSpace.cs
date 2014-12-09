using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PressSpace : MonoBehaviour {
	public Image pressSpace;
	public Text hiScore;

	void Start(){
		// iTween.PunchScale (pressSpace, iTween.Hash ("looptype", iTween.LoopType.pingPong, "time", 1f, "amount", 1.1f));
		int score = PlayerPrefs.GetInt ("HiScore");
		if(!(score > 0)){
			score = 0;
		}
		hiScore.text = "HI SCORE " + score.ToString ();
	}

	void Update(){
		if(Input.GetKeyDown (KeyCode.Space)){
			Application.LoadLevel ("MainScene");
		}
	}
}
