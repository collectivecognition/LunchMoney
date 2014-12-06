using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
	private Animator animator;
	private Vector3 speed = new Vector3(3, 0, 5);
	private float punchDistance = 1f;
	private float punchForce = 100;

	[HideInInspector] public bool onElevator = false;
	
	void Start () {
		animator = GetComponent<Animator> ();
		animator.speed = 0.5f;
	}

	void Update () {
	}

	void FixedUpdate () {
		float direction = Mathf.Sign(Input.GetAxis ("Horizontal"));
		float up = !onElevator ? Input.GetAxis ("Vertical") * speed.z : 0;
		// transform.Translate(direction * Time.deltaTime);
		rigidbody.velocity = new Vector3 (Input.GetAxis ("Horizontal") * speed.x, rigidbody.velocity.y, up);

		if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
			animator.SetBool("walking", true);
		}else{
			animator.SetBool("walking", false);
		}

		// Mirror animations

		if(Input.GetAxis("Horizontal") > 0){
			transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}

		if(Input.GetAxis("Horizontal") < 0){
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}

		// Blink randomly

		if(Random.value < 0.02){
			animator.SetTrigger("blink");
		}

		// Punch it!

		if(Input.GetKeyDown(KeyCode.Space)){
			animator.SetTrigger("punch");

			Vector3 forward = transform.TransformDirection(Vector3.left) * -direction;
			RaycastHit hit;
			if(Physics.Raycast(transform.position, forward, out hit)){
				if(hit.distance <= punchDistance){
					hit.transform.GetComponent<Enemy>().hit (punchForce * direction);
				}
			}
		}
	}

	void OnCollisionEnter(Collision collision){
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
