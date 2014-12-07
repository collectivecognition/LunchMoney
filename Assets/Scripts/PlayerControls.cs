using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
	private Animator animator;
	private Vector3 speed = new Vector3(2, 0, 5);
	private float punchDistance = 1f;
	private Vector3 punchForce = new Vector3(100f, 75f, 0f);
	private Transform carrying;
	private Vector3 carryingPosition;
	private float pickupSpeed = 7f;
	private bool pickingUp = false;
	private Vector3 pickupPosition;
	private float pickupDistance;
	private float pickupDelay = 0.15f;
	private float pickupStartTime;
	private Vector3 throwSpeed = new Vector3(1000f, 700f, 0f);

	[HideInInspector] public bool onElevator = false;
	
	void Start () {
		animator = GetComponent<Animator> ();
		animator.speed = 0.5f;
	}

	void Update () {

		// Animate picking up objects

		if(pickingUp && Time.time > pickupStartTime + pickupDelay){
			carrying.transform.position = Vector3.Lerp(pickupPosition, carryingPosition, (Time.time - pickupStartTime - pickupDelay) * pickupSpeed / pickupDistance);
			// carrying.transform.rotation = Quaternion.RotateTowards (carrying.transform.rotation, Quaternion.AngleAxis(90, Vector3.forward), pickupSpeed);
			if(carrying.transform.position == carryingPosition){
				pickingUp = false;
			}
		}
	}

	void FixedUpdate () {
			if(!pickingUp){
				float direction = transform.localEulerAngles.y == 0 ? -1 : 1;
				float up = Input.GetAxis ("Vertical") * speed.z;
	
				// Apply movement velocity

				rigidbody.velocity = new Vector3 (Input.GetAxis ("Horizontal") * speed.x, rigidbody.velocity.y, up);

				if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
					animator.SetBool("walking", true);
				}else{
					animator.SetBool("walking", false);
				}

				// Mirror animations

				if(Input.GetAxis("Horizontal") > 0){
					transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180f, transform.localEulerAngles.z);
				}

				if(Input.GetAxis("Horizontal") < 0){
					transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0f, transform.localEulerAngles.z);
				}

				// Blink randomly

				if(Random.value < 0.02){
					animator.SetTrigger("blink");
				}

				// Punch

				if(Input.GetKeyDown(KeyCode.Space)){
					animator.SetTrigger("punch");
					Vector3 forward = transform.TransformDirection(Vector3.left);
					RaycastHit hit;
					if(Physics.Raycast(transform.position, forward, out hit)){
						if(hit.distance <= punchDistance){
							hit.rigidbody.AddForce (new Vector3(punchForce.x * direction, punchForce.y, punchForce.z));
						}
					}
				}

				if(Input.GetKeyDown(KeyCode.RightShift)){

					// Throw

					if(carrying){
						carrying.transform.parent = null;
						carrying.rigidbody.detectCollisions = true;
						carrying.rigidbody.isKinematic = false;
						carrying.rigidbody.AddForce (new Vector3(throwSpeed.x * direction, throwSpeed.y, throwSpeed.z));
						animator.SetTrigger("throw");
						carrying = null;

					// Pick up

					}else{
						Transform closest = Util.FindClosestWithTag(transform, "Pickup", new Vector3(1f, 9999f, 2f));
						Debug.Log (closest);
						
						if(closest){
							pickingUp = true;
							
							// Attach to player
							
							closest.transform.parent = transform;
							
							// Turn off physics
							
							closest.rigidbody.detectCollisions = false;
							closest.rigidbody.isKinematic = true;
							
							// Set up some variables for the transition
							
							carrying = closest.transform;
							carryingPosition = new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z);
							pickupDistance = Vector3.Distance (carryingPosition, carrying.position);
							pickupStartTime = Time.time;
							pickupPosition = carrying.position;
							
							// Start animation
							
							animator.SetTrigger("carry");
						}
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
