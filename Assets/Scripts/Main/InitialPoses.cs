using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPoses : MonoBehaviour {
	public Vector3 ballInitialPosition;
	public Vector3 floorInitialPosition;
	public Vector3 globalRelativeShift;

	void Start () {
		mop.SetObjectPosition(GameObject.Find("Ball"), ballInitialPosition + globalRelativeShift);
		mop.SetObjectPosition(GameObject.Find("Ground"), floorInitialPosition + globalRelativeShift);
	}
}
