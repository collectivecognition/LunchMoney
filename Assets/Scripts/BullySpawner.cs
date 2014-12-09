using UnityEngine;
using System.Collections;

public class BullySpawner : MonoBehaviour {
	public Transform bully;

	private float spawnDelay = 15f;
	private float spawnTime = 0f;

	void Start(){
		spawnTime = Time.time - 10f;
	}

	void Update(){
		if(Time.time - spawnTime >= spawnDelay){
			spawnTime = Time.time;
			SpawnBully();
			SpawnBully();
			if(spawnTime > 5f){
				spawnTime -= 0.5f;
			}
		}
	}

	void SpawnBully(){
		Transform newBully = (Transform)GameObject.Instantiate(bully);
		int floor = 0;
		newBully.transform.position = Util.RandomPositionOnFloor(0);
	}
}
