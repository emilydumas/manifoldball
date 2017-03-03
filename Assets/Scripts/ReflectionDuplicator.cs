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
							/*
							new Quaternion(
								target.transform.localRotation.x * (float) Math.Pow(-1, i % 2),
								target.transform.localRotation.y * (float) Math.Pow(-1, j % 2),
								target.transform.localRotation.z * (float) Math.Pow(-1, k % 2),
								target.transform.localRotation.w * (float) Math.Pow(-1, i % 2) * (float) Math.Pow(-1, j % 2) * (float) Math.Pow(-1, k % 2)
							)

							new Quaternion(
								target.transform.localRotation.x*-1.0f,
								target.transform.localRotation.y,
								target.transform.localRotation.z,
								target.transform.localRotation.w * -1.0f)
								*/
							//Quaternion.Euler(-target.transform.rotation.eulerAngles)
							//Quaternion.Inverse(target.transform.rotation)
							target.transform.rotation
						);
						clone.transform.parent = clonegroup.transform;
					}
		}

		//UnityEngine.VR.InputTracking.Recenter ();

		//GameObject.Find ("Player").transform.position.Set (0f, -0.6f, 0f);
	}
}
