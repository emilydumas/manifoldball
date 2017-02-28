using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedParameters : MonoBehaviour {
	// Unit: meter(s)
	public float cubeSize = 3;

	// Any parameter that need to be used by multiple scripts should go here

	// Use this for initialization
	void Start () {
		
		/*
		 * Sample code: how to access a variable from another script file
		 * This example is written for Wrapper.cs to access variables in Duplicator.cs
		 
		// Access variables from another script
		Duplicator sphere = GameObject.Find("Runner").GetComponent<Duplicator>();
		Debug.LogWarning ("From Wrapper: Getting values from Runner's Duplicator:"
			+ " N = " + sphere.N
			+ ", cubesize = " + sphere.cubesize
			+ ", targetTag = '" + sphere.targetTag + "'"
			// + ", clonecount = " + sphere.clonecount    // private variable
		);
		
		*/

	}
}
