using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private bool onElevator;
	private Animator animator;
	private float hp = 3;

	void Start () {
		animator = GetComponent<Animator> ();
		animator.speed = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void hit(float force){
		animator.SetTrigger ("hit");
		rigidbody.AddForce (new Vector3 (force / 2, Mathf.Abs(force), 0));
		hp -= 1;
		if(hp <= 0){
			Destroy (gameObject);
		}
	}

}
