using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	public Transform player;
	private PlayerControls playerControls;
	private float speed = 1f;
	private int floor = 0;
	private int targetFloor = -1;
	private float startTime;
	private float distance;
	private float[] floors = new float[] {1f, 4.85f, 8.7f};

	// Use this for initialization
	void Start () {
		playerControls = (PlayerControls)player.GetComponent<PlayerControls> ();
	}

	void ChangeFloor (int newFloor){
		targetFloor = newFloor;
		startTime = Time.time;
		distance = Mathf.Abs (newFloor - floor);
	}

	// Update is called once per frame

	void Update () {
		if(playerControls.onElevator){
			if(targetFloor == -1){
				if(Input.GetKeyDown (KeyCode.Alpha1)){
					ChangeFloor (0);
				}

				if(Input.GetKeyDown (KeyCode.Alpha2)){
					ChangeFloor (1);
				}

				if(Input.GetKeyDown (KeyCode.Alpha3)){
					ChangeFloor (2);
				}
			}
		}

		if(targetFloor != -1){
			float y = Mathf.Lerp(floors[floor], floors[targetFloor], (Time.time - startTime) * speed / distance);
			transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);

			if(transform.localPosition.y == floors[targetFloor]){
				floor = targetFloor;
				targetFloor = -1;
			}
		}
	}
}