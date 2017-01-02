using UnityEngine;
using System.Collections;

public class Edible : MonoBehaviour {
	public void Eat(){
		GetComponent<Collider>().enabled = false;
		if(GetComponent<Rigidbody>()){
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		}
		iTween.FadeTo(gameObject, iTween.Hash("alpha", 0f, "time", 0.1f, "oncomplete", "Remove"));

	}

	public void Remove(){
		GameObject.Destroy (gameObject);
	}
}
