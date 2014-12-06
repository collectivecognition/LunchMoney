using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
	private CharacterController controller;
	private Vector3 speed = new Vector3(3, 0, 5);

	[HideInInspector] public bool onElevator = false;
	
	void Start () {
	}

	void Update () {
	}

	void FixedUpdate () {
		Vector3 direction = Vector3.zero;
		float up = !onElevator ? Input.GetAxis ("Vertical") * speed.z : 0;
		direction = new Vector3 (Input.GetAxis ("Horizontal") * speed.x, rigidbody.velocity.y, up);
		// transform.Translate(direction * Time.deltaTime);
		rigidbody.velocity = direction;
	}

	void OnCollisionEnter(Collision collision){
		Debug.Log (collision.collider.tag);
		if(collision.collider.tag == "Elevator"){
			transform.parent = collision.collider.transform;
			onElevator = true;
		}
	}

	void OnCollisionExit(Collision collision){
		if(collision.collider.tag == "Elevator"){
			transform.parent = null;
			onElevator = false;
		}
	}
}
