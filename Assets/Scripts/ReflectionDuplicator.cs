using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionDuplicator : Duplicator
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
	}

	protected GameObject TransformedClone(GameObject target, Vector3 translation, Vector3 scale, Quaternion rotation)
	{
		// Make a full hierarchical clone of the input object and all components
		GameObject clone = Instantiate(target);
		clone.name = target.name + "-clone" + clonecount;

		// Disable all scripts on the clone
		// based on http://answers.unity3d.com/questions/292802
		MonoBehaviour[] scripts = clone.GetComponents<MonoBehaviour>();
		foreach (MonoBehaviour script in scripts)
		{
			Destroy(script);
		}

		// Copies are not rigid bodies, either
		Rigidbody[] rigidBodies = clone.GetComponents<Rigidbody>();
		foreach (Rigidbody rb in rigidBodies)
		{
			//if (target.name != "Sphere")
				Destroy(rb);
		}

		// Remove the colliders, too
		BoxCollider[] boxColliders = clone.GetComponents<BoxCollider>();
		foreach (BoxCollider bc in boxColliders)
		{
			Destroy(bc);
		}

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
