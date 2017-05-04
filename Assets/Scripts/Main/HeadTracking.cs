using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTracking : MonoBehaviour {
	public GameObject OVRCamera;

	private Material rend;
	private Vector3 CamPose;
	private Quaternion CamRot;
	private Vector3 headScal;

	void Start()
	{
		headScal = new Vector3 (1, 1, 1);
		rend = this.GetComponent<Renderer> ().sharedMaterial;
	}

	// Update is called once per frame
	void Update () 
	{
		CamRot = OVRCamera.transform.localRotation;
		CamPose=OVRCamera.transform.localPosition;
		Matrix4x4 Hpose = rend.GetMatrix ("_objectpose");
		Hpose = Matrix4x4.TRS (CamPose, CamRot, headScal);
		rend.SetMatrix ("_objectpose",Hpose);
	}
}
