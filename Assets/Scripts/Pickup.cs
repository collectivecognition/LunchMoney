using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	private Vector3 throwSpeed = new Vector3(700f, 200f, 0f);
	private float layerResetDelay = 2f;
	private float layerResetTime = 0f;
	public AudioClip thudSound;
	
	void Update () {
		if(Time.time >= layerResetTime + layerResetDelay){
			gameObject.layer = LayerMask.NameToLayer ("Pickups");
		}
	}

	public void Throw(float direction) {
		transform.parent = null;
		rigidbody.detectCollisions = true;
		rigidbody.isKinematic = false;
		rigidbody.AddForce (new Vector3(throwSpeed.x * direction, throwSpeed.y, throwSpeed.z));
		gameObject.layer = LayerMask.NameToLayer ("CollidablePickups");
		layerResetTime = Time.time;
	}

	void OnCollisionEnter(Collision collision){
		if(collision.collider.name != "Girl" & collision.relativeVelocity.magnitude > 2f){
			audio.PlayOneShot (thudSound);
		}
	}
}
