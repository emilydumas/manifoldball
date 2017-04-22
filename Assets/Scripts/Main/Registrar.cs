/// <summary>
/// Initialize the VRMOP shaders and setup the hierarchy of linked object clones and per-renderer properties
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Registrar : MonoBehaviour {
	public string targetTag = "Geometric";
	public float IPD = 0.06f;
	public CameraController cameraController;

	// TODO: Separate clone logic and variables into another script/class
	public int N = 2;
	private float cubesize; // Fetched from wrapper
	private int clonecount = 0;

	void Awake()
	{
		// STEP 1: SHADER INITIALIZATION
		// Every instance of VRMOP needs projection and eye shift matrices.
	
		// Retrieve the current projection
		Matrix4x4 currentP = GL.GetGPUProjectionMatrix(Camera.main.projectionMatrix, true);
		// Create a standard view matrix
		Matrix4x4 defaultV = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, 1, -1));

		var VP0 = currentP * defaultV;

		// 6cm Interpupillary Distance assumed
		Matrix4x4 eye0shift = Matrix4x4.TRS(new Vector3(0.5f*IPD,0,0), Quaternion.identity, new Vector3(1,1,1));
		Matrix4x4 eye1shift = Matrix4x4.TRS(new Vector3(-0.5f*IPD,0,0), Quaternion.identity, new Vector3(1,1,1));

		foreach (GameObject target in GameObject.FindGameObjectsWithTag(targetTag)) {
			Renderer rend = target.GetComponent<Renderer> ();

			// First we duplicate so changed properties will not be saved.
			rend.sharedMaterial = new Material (rend.sharedMaterial);

			// TODO: Move these into MOPUtil?
			// Now we initialize their locks and PV matrices
			rend.sharedMaterial.SetMatrix("_VP0", VP0);
			rend.sharedMaterial.SetInt ("_locked", 1);

			rend.sharedMaterial.SetMatrix ("_eye0shift", eye0shift);
			rend.sharedMaterial.SetMatrix ("_eye1shift", eye1shift);

			// Disable culling
			Mesh mesh = target.GetComponent<MeshFilter>().mesh;
			if (mesh != null) {
				mesh.bounds = new Bounds (new Vector3 (0, 0, 0), new Vector3 (float.MaxValue, float.MaxValue, float.MaxValue));
			} else {
				Debug.Log ("Failed to update bounds on \"" + target.name + "\", no MeshFilter component found.");
			}
		}
	}


	void Start()
	{
		// STEP 2: REGISTER FOR CAMERA EVENTS

		// Each instance of VRMOP needs to receive camera pose updates; put them in the
		// queue for updates by CameraController.

		// Note that this is done before duplication, so a single queue entry updates the camera
		// pose for all of the linked clones.

		if (cameraController != null) {
			foreach (GameObject target in GameObject.FindGameObjectsWithTag(targetTag)) {
				Renderer rend = target.GetComponent<Renderer> ();

				// Now we store a reference to the shared material in the camera controller
				cameraController.AddCameraPoseTargetMaterial (rend);

				Mesh mesh = target.GetComponent<MeshFilter> ().mesh;
				if (mesh != null) {
					mesh.bounds = new Bounds (new Vector3 (0, 0, 0), new Vector3 (float.MaxValue, float.MaxValue, float.MaxValue));
				} else {
					Debug.Log ("Failed to update bounds on \"" + target.name + "\", no MeshFilter component found.");
				}
			}
		} else {
			Debug.LogError ("Null cameraController; unable to register any objects.");
		}

		// STEP 3: CREATE CLONES
		cubesize = this.GetComponent<Wrapper> ().cubesize;

		//physicsUtil = this.GetComponent<PhysicsUtil> ();

		foreach (GameObject target in GameObject.FindGameObjectsWithTag(targetTag))
		{
			ResetCloneCount ();
			GameObject clonegroup = new GameObject(target.name + "-clones");
	
			for (int i = -N; i <= N; i++)
				for (int j = -N; j <= N; j++)
					for (int k = -N; k <= N; k++) {
						if (i == 0 && j == 0 && k == 0)
							continue;
						GameObject clone = TransformedClone (
							                   target,
							                   new Vector3 (cubesize * i, cubesize * j, cubesize * k)
						                   );
						clone.transform.parent = clonegroup.transform;
					}
		}
	}
		
	private void ResetCloneCount() {
		clonecount = 0;
	}

	// TODO: Matrix4x4 transform parameter instead of translation vector
	private GameObject TransformedClone(GameObject target, Vector3 translation)
	{
		// Make a full hierarchical clone of the input object...
		GameObject clone = Instantiate(target);
		clone.name = target.name + "-clone" + clonecount;

		// ...but now remove the unnecessary components of the clone.
		RemoveComponents(clone);

		// Set FollowerPose of the clone as a per-rendered property
		mop.SetFollowerPose(clone, Matrix4x4.TRS(translation,Quaternion.identity, new Vector3(1,1,1)));

		// Disable culling
		Mesh mesh = clone.GetComponent<MeshFilter>().mesh;
		if (mesh != null) {
			mesh.bounds = new Bounds (new Vector3 (0, 0, 0), new Vector3 (float.MaxValue, float.MaxValue, float.MaxValue));
		} else {
			Debug.Log ("Failed to update bounds on \"" + clone.name + "\", no MeshFilter component found.");
		}

		clonecount++;
		return clone;
	}

	private void RemoveComponents(GameObject target) {
		// Strip all scripts, colliders, rigid bodies from a gameobject

		// Remove scripts
		// based on http://answers.unity3d.com/questions/292802
		MonoBehaviour[] scripts = target.GetComponents<MonoBehaviour>();
		foreach (MonoBehaviour script in scripts)
		{
			Destroy(script);
		}
		
		// Remove rigid bodies
		Rigidbody[] rigidBodies = target.GetComponents<Rigidbody>();
		foreach (Rigidbody rb in rigidBodies)
		{
			Destroy(rb);
		}

		// Remove box colliders
	    // TODO: Support all types of colliders
		BoxCollider[] boxColliders = target.GetComponents<BoxCollider>();
		foreach (BoxCollider bc in boxColliders)
		{
			Destroy(bc);
		}		
	}

}