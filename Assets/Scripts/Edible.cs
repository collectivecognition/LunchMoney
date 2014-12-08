using UnityEngine;
using System.Collections;

public class Edible : MonoBehaviour {
	public void Eat(){
		collider.enabled = false;
		if(rigidbody){
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}
		iTween.FadeTo(gameObject, iTween.Hash("alpha", 0f, "time", 0.1f, "oncomplete", "Remove"));

	}

	public void Remove(){
		GameObject.Destroy (gameObject);
	}
}
