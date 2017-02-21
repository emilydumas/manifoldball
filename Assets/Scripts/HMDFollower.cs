using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMDFollower : MonoBehaviour {
    public OVRCameraRig cameraRig;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = cameraRig.leftEyeAnchor.localPosition;
        transform.localRotation = cameraRig.leftEyeAnchor.localRotation;      
    }
}
