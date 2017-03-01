using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionWrapper : Wrapper {
	protected float cellsize = 4.0f;

	// Use this for initialization
	void Start () {
		//initial_diff = gameObject.transform.position - wrap(gameObject.transform.position);
		cellsize = GameObject.Find("Runner").GetComponent<SharedParameters>().cubeSize;
	}

	// Returns the normal vector of the wall that our ball is heading toward.
	// The approach is to find the smallest angle between the ball's velocity
	// and the normal vector of the walls.
	// param: 
	//   b: Velocity vector of the ball
	private Vector3 wallNormal(Vector3 b) {
		Vector3[] wallNormal = new [] {
			new Vector3 (0f, -1f, 0f),    // top
			new Vector3 (0f, 1f, 0f),     // bottom
			new Vector3 (0f, 0f, -1f),    // front
			new Vector3 (0f, 0f, 1f),     // back
			new Vector3 (1f, 0f, 0f),     // left
			new Vector3 (-1f, 0f, 0f)     // right
		};

		Vector3 closestVector = wallNormal[0];
		float smallestAngle = Vector3.Angle(b, wallNormal[0]);

		for (int i = 1; i < 5; i++) {
			float angle = Vector3.Angle (b, wallNormal [i]);
			if (angle < smallestAngle) {
				angle = smallestAngle;
				closestVector = wallNormal[i];
			}
		}

		return closestVector;
	}

	
	// Update is called once per frame
	void Update () {
		// Normal vector of the wall that the ball is heading toward
		Debug.LogWarning(gameObject);
		Vector3 wallNormalVector = wallNormal( new Vector3(-3, 12, 13));
		Debug.LogWarning (wallNormalVector);
	}
}
