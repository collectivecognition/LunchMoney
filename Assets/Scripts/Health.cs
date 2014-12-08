using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	public Image turkey1;
	public Image turkey2;
	public Image turkey3;
	public Image turkey4;
	public Image turkey5;
	public Sprite whole;
	public Sprite half;
	public Sprite empty;
	public Transform player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float health = (player.GetComponent <PlayerControls> ()).health;
		if(health < 0.5){ turkey1.sprite = empty; }
		if(health >= 0.5){
			turkey1.sprite = half;
		}
		if(health >= 1){
			turkey1.sprite = whole;
		}

		if(health < 1.5){ turkey2.sprite = empty; }
		if(health >= 1.5){
			turkey2.sprite = half;
		}
		if(health >= 2){
			turkey2.sprite = whole;
		}

		if(health < 2.5){ turkey3.sprite = empty; }
		if(health >= 2.5){
			turkey3.sprite = half;
		}
		if(health >= 3){
			turkey3.sprite = whole;
		}

		if(health < 3.5){ turkey4.sprite = empty; }
		if(health >= 3.5){
			turkey4.sprite = half;
		}
		if(health >= 4){
			turkey4.sprite = whole;
		}

		if(health < 4.5){ turkey5.sprite = empty; }
		if(health >= 4.5){
			turkey5.sprite = half;
		}
		if(health >= 5){
			turkey5.sprite = whole;
		}
	}
}
