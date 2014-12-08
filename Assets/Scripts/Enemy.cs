﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private GameObject player;
	public Transform coin;
	public AudioClip hitSound;
	public AudioClip dieSound;

	private bool onElevator;
	private Animator animator;
	[HideInInspector] public float health = 3;
	private float punchDelay = 0.25f;
	private float punchTime = 0f;
	private float hitDelay = 0.7f;
	private float hitTime = 0f;
	private bool dead = false;
	private float blinkDelay = 0.1f;
	private float blinkTime = 0f;
	private int blinkTimes = 8;
	private int blinkCount = 0;
	private string state = null;
	private float waitTime;
	private float waitDelay = 0;
	private Vector3 wanderPosition = Vector3.zero;
	private Vector3 wanderSpeed = new Vector3(1f, 0, 2f);
	private Vector3 attackSpeed = new Vector3(3f, 0, 2f);

	void Start () {
		animator = GetComponent<Animator> ();
		animator.speed = 0.5f;

		player = GameObject.Find ("Girl");
	}

	void Update () {

		if(!dead){

			// Pick a new state

			if(state == null){
				float r = Random.value;
				
				if(r <= 0.5f){
					state = "waiting";
				}
				
				if(r > 0.5f && r < 0.75f){
					state = "wandering";
				}
				
				if(r >= 0.75f){
					state = "attacking";
				}
			}

			if((hitTime == 0 || Time.time - hitTime > hitDelay)){
				if(state == "attacking"){
					ComeAtMeBro();
				}

				if(state == "waiting"){
					Wait ();
				}

				if(state == "wandering"){
					Wander();
				}

				if(state == "punching"){
					Punch ();
				}
			}
		}else{

			// Do some dying

			if(Time.time > blinkTime + blinkDelay){
				renderer.enabled = false;
			}

			if(Time.time > blinkTime + blinkDelay * 2){
				renderer.enabled = true;
				blinkCount++;
				blinkTime = Time.time;
			}

			if(blinkCount > blinkTimes){
				GameObject.Destroy(gameObject);

				// Spawn a coin when dead
				
				Transform newCoin = (Transform)GameObject.Instantiate(coin);
				newCoin.position = transform.position;
				newCoin.rigidbody.AddForce(new Vector3(0f, 300f, 0f));
			}
		}
	}

	void MoveToward(Vector3 position, Vector3 speed){
		animator.SetBool("walking", true);

		transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, position.x, speed.x * Time.deltaTime), transform.position.y, Mathf.MoveTowards(transform.position.z, position.z, speed.z * Time.deltaTime));
		Face (position);
	}

	void Face(Vector3 target){
		if(transform.position.x < target.x){
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180f, transform.localEulerAngles.z);
		}else{
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0f, transform.localEulerAngles.z);
		}
	}

	void FixedUpdate () {

		// Blink randomly
		
		if(Random.value < 0.02){
			animator.SetTrigger("blink");
		}
	}

	private void Wait(){
		animator.SetBool ("walking", false);
		Face (player.transform.position);

		if(waitDelay == 0){
			waitDelay = Random.Range (1f, 3f);
			waitTime = Time.time;
		}

		if(Time.time - waitTime >= waitDelay){
			waitDelay = 0;
			state = null;
		}
	}

	private void Wander() {

		// Choose a target

		if(wanderPosition == Vector3.zero){
			Vector3 randPos = Util.RandomPositionOnFloor(0);
			randPos.y = transform.position.y;
			if(randPos.y < transform.position.y - 1f){
				randPos.y = transform.position.y - 1f;
			}
			if(randPos.y > transform.position.y + 1f){
				randPos.y = transform.position.y + 1f;
			}

			wanderPosition = randPos;
		}

		MoveToward (wanderPosition, wanderSpeed);

		if(transform.position.x == wanderPosition.x && transform.position.z == wanderPosition.z){
			state = null;
			wanderPosition = Vector3.zero;
		}
	}

	private void Punch(){
		if(Time.time > punchTime + punchDelay){
			animator.SetTrigger ("punch");

			float distance = Vector3.Distance (transform.position, player.transform.position);
			if(distance <= 0.5f){
				player.GetComponent<PlayerControls>().Hit(0.5f, Mathf.Sign (player.transform.position.x - transform.position.x));
			}

			state = "wandering";
			wanderPosition = Vector3.zero;
		}
	}

	private void ComeAtMeBro() {
		float distance = Vector3.Distance (transform.position, player.transform.position);
		if(distance > 0.5f){
			MoveToward (player.transform.position, attackSpeed);
		}else{
			animator.SetBool ("walking", false);
			state = "punching";
			punchTime = Time.time;
		}
	}
	
	public void Hit(float damage, float direction){
		health -= damage;
		hitTime = Time.time;

		if(health > 0){
			audio.PlayOneShot(hitSound);
			Vector3 dir = new Vector3 (10f * direction, 10f, 0f);
			rigidbody.AddForce (dir);
			animator.SetTrigger ("hurt");
		}else{
			audio.PlayOneShot(dieSound);
			dead = true;
			blinkTime = Time.time + 2f;
			animator.SetTrigger ("kill");
			Vector3 dir = new Vector3 (20f * direction, 10f, 0f);
			rigidbody.AddForce (dir);
		}
	}

	void OnCollisionEnter(Collision collision){
		if(collision.collider.tag == "Pickup"){
			if(collision.relativeVelocity.magnitude > 3f){
				Hit (3f, Mathf.Sign(collision.relativeVelocity.x));
			}
		}
	}
}
