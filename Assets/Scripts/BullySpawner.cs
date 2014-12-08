using UnityEngine;
using System.Collections;

public class BullySpawner : MonoBehaviour {
	public Transform bully;

	private float spawnDelay = 30f;
	private float spawnTime = 0f;

	void Start(){
		spawnTime = Time.time;

		SpawnBully();
		SpawnBully();
		SpawnBully();
		SpawnBully();
		SpawnBully();
		SpawnBully();
	}

	void Update(){
		if(Time.time - spawnTime >= spawnDelay){
			spawnTime = Time.time;
			// SpawnBully();
		}
	}

	void SpawnBully(){
		Transform newBully = (Transform)GameObject.Instantiate(bully);
		int floor = 0;
		newBully.transform.position = Util.RandomPositionOnFloor(0);
	}
}
