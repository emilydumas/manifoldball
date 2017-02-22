using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour {

    public OVRInput.Controller RightController;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "RightHand") GetComponent<Rigidbody>().AddForce(OVRInput.GetLocalControllerVelocity(RightController) * 1, ForceMode.VelocityChange);
    }
}
