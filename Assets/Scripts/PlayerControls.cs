using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
	private CharacterController controller;
	private Vector3 speed = new Vector3(3, 0, 5);
	
	void Start () {
		// controller = GetComponent<CharacterController>();
	}

	void Update () {
	}

	void FixedUpdate () {
		Vector3 direction = Vector3.zero;
		direction = new Vector3 (Input.GetAxis ("Horizontal") * speed.x, 0, Input.GetAxis ("Vertical") * speed.z);
		// transform.Translate(direction * Time.deltaTime);
		rigidbody.velocity = direction;
	}
}
