using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDuplicator : Duplicator
{
	void Start()
	{
		cubesize = GameObject.Find("Runner").GetComponent<SharedParameters>().cubeSize;

		foreach (GameObject target in GameObject.FindGameObjectsWithTag(targetTag))
		{
			Vector3 targetPosition = target.transform.position;
				
			GameObject clonegroup = new GameObject(target.name + "-clones");
			clonegroup.transform.position = new Vector3(0, 0, 0);
			clonegroup.transform.localScale = new Vector3(1, 1, 1);
			clonegroup.transform.localRotation = new Quaternion();

			for (int i = -N; i <= N; i++)
				for (int j = -N; j <= N; j++)
					for (int k = -N; k <= N; k++)
					{
						if (i == 0 && j == 0 && k == 0)
							continue;

						GameObject clone = TransformedClone(
							target,
							new Vector3 (  // position
								cubesize * i + (float) Math.Pow(-1, i) * targetPosition.x, 
								cubesize * j + (float) Math.Pow(-1, j) * targetPosition.y, 
								cubesize * k + (float) Math.Pow(-1, k) * targetPosition.z
							),
							new Vector3 (  // orientation
								(float) Math.Pow(-1, i % 2), 
								(float) Math.Pow(-1, j % 2),
								(float) Math.Pow(-1, k % 2)
							),
							new Quaternion()
						);
						clone.transform.parent = clonegroup.transform;
					}
		}



		/* Add rigidBody and scripts to the actual player */

		// RacketParent
		// TouchController
		TouchController racketController = GameObject.Find ("RacketParent").AddComponent<TouchController>() as TouchController;
		racketController.controller = OVRInput.Controller.RTouch;

		// LowPolygonRacket
		// Rigidbody
		Rigidbody racketRigidBody = GameObject.Find ("LowPolygonRacket").AddComponent<Rigidbody>() as Rigidbody;
		racketRigidBody.isKinematic = true;
		racketRigidBody.useGravity = false;
		// MeshCollider
		MeshCollider meshCol = GameObject.Find ("LowPolygonRacket").AddComponent<MeshCollider>() as MeshCollider;
		meshCol.material = (UnityEngine.PhysicMaterial)Resources.Load ("Materials/Bounce");

		// LeftHand
		// TouchController
		TouchController leftHandController = GameObject.Find ("LeftHand").AddComponent<TouchController>() as TouchController;
		leftHandController.controller = OVRInput.Controller.LTouch;
		// Grab
		Grab grabber = GameObject.Find ("LeftHand").AddComponent<Grab>() as Grab;
		grabber.controller = OVRInput.Controller.LTouch;
		grabber.grabRadius = 0.3f;
		grabber.grabButton = "LGrab";
		//grabber.grabMask = LayerMask.NameToLayer("Ball");

		// Head
		// HMDFollower
		HMDFollower hmdFollower = GameObject.Find ("Head").AddComponent<HMDFollower>() as HMDFollower;
		// Thanks to this answer... I'm now able to locate OVRCameraRig
		// http://answers.unity3d.com/answers/492773/view.html
		GameObject ovrCameraObj = GameObject.Find ("OVRCameraRig");
		if (ovrCameraObj != null)
			hmdFollower.cameraRig = ovrCameraObj.GetComponent<OVRCameraRig>();
		// MeshCollider
		MeshCollider headMeshCol = GameObject.Find ("Head").AddComponent<MeshCollider>() as MeshCollider;
		headMeshCol.material = (UnityEngine.PhysicMaterial)Resources.Load ("Materials/Bounce");
		//headMeshCol.sharedMesh = (Mesh)Resources.Load(  // Cube

		// Sphere
		GameObject sphere = GameObject.Find ("Sphere");
		// Rigidbody
		Rigidbody sphereRigidBody = sphere.AddComponent<Rigidbody> () as Rigidbody;
		sphereRigidBody.mass = 1f;
		sphereRigidBody.drag = 0.1f;
		sphereRigidBody.angularDrag = 0.05f;
		sphereRigidBody.isKinematic = false;
		sphereRigidBody.useGravity = false;
		// Position Resetter
		PositionResetter positionResetter = sphere.AddComponent<PositionResetter>() as PositionResetter;
		positionResetter.controller = OVRInput.Controller.RTouch;
		// Wrapper
		sphere.AddComponent<Wrapper>();
		// Highlighter
		sphere.AddComponent<Highlighter>();
		// Manual Bounce Physics
		ManualBouncePhysics manualBouncePhysics = sphere.AddComponent<ManualBouncePhysics>() as ManualBouncePhysics;
		manualBouncePhysics.forceMultiplier = 1;
		manualBouncePhysics.racketController = OVRInput.Controller.RTouch;
		manualBouncePhysics.racketGameObject = GameObject.Find ("LowPolygonRacket");
		// Brakes
		Brakes brakes = sphere.AddComponent<Brakes>() as Brakes;
		brakes.rb = sphere.GetComponent<Rigidbody>();
	}

	protected GameObject TransformedClone(GameObject target, Vector3 translation, Vector3 scale, Quaternion rotation)
	{
		// Make a full hierarchical clone of the input object and all components
		GameObject clone = Instantiate(target);
		clone.name = target.name + "-clone" + clonecount;

		// Disable all scripts on the clone
		// based on http://answers.unity3d.com/questions/292802
		/*
		MonoBehaviour[] scripts = clone.GetComponents<MonoBehaviour>();
		foreach (MonoBehaviour script in scripts)
		{
			Destroy(script);
		}

		// Copies are not rigid bodies, either
		Rigidbody[] rigidBodies = clone.GetComponents<Rigidbody>();
		foreach (Rigidbody rb in rigidBodies)
		{
			Destroy(rb);
		}

		// Remove the colliders, too
		BoxCollider[] boxColliders = clone.GetComponents<BoxCollider>();
		foreach (BoxCollider bc in boxColliders)
		{
			Destroy(bc);
		}
		*/

		// Attach the TransformFollow script
		TransformFollower tf = clone.AddComponent<TransformFollower>() as TransformFollower;
		tf.translation = translation;
		tf.scale = scale;
		tf.rotation = rotation;
		tf.leader = target;

		clonecount++;
		return clone;
	}
}
