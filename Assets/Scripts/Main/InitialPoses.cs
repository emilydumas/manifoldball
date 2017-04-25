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
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space)||Input.GetAxis("Reset")==1) 
		{
			mop.SetObjectPosition (GameObject.Find ("Ball"), ballInitialPosition + globalRelativeShift);
			//mop.TranslateObjectPosition (GameObject.Find ("Ball").GetComponent<Renderer> (),new Vector3(0,0,0));
		}
	}
}
	