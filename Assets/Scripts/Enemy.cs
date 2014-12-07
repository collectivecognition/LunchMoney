using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public Transform player;

	private bool onElevator;
	private Animator animator;
	private float hp = 3;
	private bool attacking = true;
	private float speed = 1f;

	void Start () {
		animator = GetComponent<Animator> ();
		animator.speed = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if(attacking){
			comeAtMeBro();
		}else{
			wander();
		}
	}

	void FixedUpdate () {

		// Blink randomly
		
		if(Random.value < 0.02){
			animator.SetTrigger("blink");
		}
	}

	private void wander() {
	}

	private void comeAtMeBro() {
		float distance = Vector3.Distance (transform.position, player.position);
		if(distance > 2f){
			transform.position = Vector3.MoveTowards (transform.position, player.position, speed * Time.deltaTime);
			animator.SetBool("walking", true);
		}else{
			animator.SetBool ("walking", false);
		}
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
