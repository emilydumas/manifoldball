// Based on the tutorial "Grabbing & throwing objects with Oculus Touch"
// by Ben Roberts
// https://www.youtube.com/watch?v=mFFta9OszzA

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {
    public OVRInput.Controller controller;
    public float grabRadius;
    public LayerMask grabMask;

    private GameObject grabbedObject;
    private bool grabbing;
    private bool oldKinematic;
    
    void GrabObject()
    {
        grabbing = true;

        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0f, grabMask);
        if (hits.Length > 0)
        {
            int closestHit = 0;
            for (int i=0; i < hits.Length; i++)
            {
                if (hits[i].distance < hits[closestHit].distance) closestHit = i;
            }
            grabbedObject = hits[closestHit].transform.gameObject;
            oldKinematic = grabbedObject.GetComponent<Rigidbody>().isKinematic;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void DropObject()
    {
        grabbing = false;

        if (grabbedObject != null)
        {
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false; //oldKinematic;
            grabbedObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(controller);
            Vector3 omega = OVRInput.GetLocalControllerAngularVelocity(controller).eulerAngles * Mathf.Deg2Rad;
            grabbedObject.GetComponent<Rigidbody>().angularVelocity = omega;
            grabbedObject = null;
        }
    }
	
	void Update () {
        float b = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);

        if (!grabbing && b == 1) GrabObject();
        
        if (grabbing && b < 1) DropObject();

        if (grabbing && grabbedObject != null)
        {
            grabbedObject.transform.position = transform.position;
            grabbedObject.transform.rotation = transform.localRotation;
        }
    }
}