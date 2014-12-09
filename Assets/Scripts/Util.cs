using UnityEngine;
using System.Collections;

public class Util
{
	public static float floorMinX = -9f;
	public static float floorMaxX = 9f;
	public static float[] floorMinZ = new float[] {14.6f};
	public static float[] floorMaxZ = new float[] {-7.7f};

	public static Transform FindClosestWithTag(Transform player, string tag, Vector3 maxDistance){
		GameObject[] pickups = GameObject.FindGameObjectsWithTag(tag);
		Transform closest = null;
		float closestDistance = Mathf.Infinity;
		for(int ii = 0; ii < pickups.Length; ii++){
			Transform pickup = pickups[ii].transform;
			float distance = Vector3.Distance (player.position, pickup.position);
			float xDistance = Mathf.Abs (player.position.x - pickup.position.x);
			float yDistance = Mathf.Abs (player.position.y - pickup.position.y);
			float zDistance = Mathf.Abs (player.position.z - pickup.position.z);
			Debug.Log (distance + ":" + xDistance + ":" + zDistance + ":" + pickup.tag);
			if(distance < closestDistance){
				if((player.right.x > 0 && pickup.position.x < player.position.x) || (player.right.x < 0 && pickup.position.x > player.position.x)){
					if(xDistance < maxDistance.x && yDistance < maxDistance.y && zDistance < maxDistance.z){
						closest = pickup;
						closestDistance = distance;
					}
				}
			}
		}

		return closest;
	}

	public static Vector3 RandomPositionOnFloor(int floor){
		return new Vector3(Random.Range(floorMinX, floorMaxX), 0, Random.Range (floorMinZ[floor], floorMaxZ[floor]));
	}
}
