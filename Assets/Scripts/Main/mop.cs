using System;
using UnityEngine;

public static class mop {
	public static Vector4 _exone(Vector3 v)
	{
		return new Vector4 (v.x, v.y, v.z, 1.0f);
	}

	public static void SetFollowerPose(Renderer ren, Matrix4x4 P)
	{
		MaterialPropertyBlock propblock = new MaterialPropertyBlock();

		ren.GetPropertyBlock(propblock);
		propblock.SetVector("_followerpose1", P.GetRow(0));
		propblock.SetVector("_followerpose2", P.GetRow(1));
		propblock.SetVector("_followerpose3", P.GetRow(2));
		propblock.SetVector("_followerpose4", P.GetRow(3));
		ren.SetPropertyBlock (propblock);
	}

	public static void SetFollowerPose(GameObject obj, Matrix4x4 P)
	{
		Renderer ren = obj.GetComponent<Renderer>();
		SetFollowerPose(ren,P);
	}

	public static Matrix4x4 GetFollowerPose(Renderer ren)
	{
		Matrix4x4 M = new Matrix4x4 ();

		MaterialPropertyBlock propblock = new MaterialPropertyBlock ();
		ren.GetPropertyBlock(propblock);
		M.SetRow (0, propblock.GetVector ("_followerpose1"));
		M.SetRow (1, propblock.GetVector ("_followerpose2"));
		M.SetRow (2, propblock.GetVector ("_followerpose3"));
		M.SetRow (3, propblock.GetVector ("_followerpose4"));
		return(M);
	}

	public static Matrix4x4 GetFollowerPose(GameObject obj)
	{
		Renderer ren = obj.GetComponent<Renderer> ();
		return GetFollowerPose (ren);
	}

	public static Matrix4x4 GetObjectPose(Renderer ren)
	{
		return ren.sharedMaterial.GetMatrix ("_objectpose");
	}

	public static Matrix4x4 GetObjectPose(GameObject obj)
	{
		Renderer ren = obj.GetComponent<Renderer>();
		return GetObjectPose(ren);
	}

	public static void SetObjectPose(Renderer ren, Matrix4x4 P)
	{
		ren.sharedMaterial.SetMatrix ("_objectpose",P);
	}

	public static void SetObjectPose(GameObject obj, Matrix4x4 P)
	{
		Renderer ren = obj.GetComponent<Renderer> ();
		SetObjectPose (ren, P);
	}

	public static void MultiplyObjectPose(Renderer ren, Matrix4x4 T)
	{
		Matrix4x4 P = GetObjectPose (ren);
		SetObjectPose (ren, T * P);
	}

	public static void MultiplyObjectPose(GameObject obj, Matrix4x4 T)
	{
		Renderer ren = obj.GetComponent<Renderer> ();
		MultiplyObjectPose (ren, T);
	}

	public static Vector3 GetObjectPosition(Renderer ren)
	{
		Matrix4x4 P = GetObjectPose (ren);
		return P.GetColumn (3);
	}

	public static Vector3 GetObjectPosition(GameObject obj)
	{
		Renderer ren = obj.GetComponent<Renderer> ();
		return GetObjectPosition (ren);
	}

	public static void SetObjectPosition(Renderer ren, Vector3 pt)
	{
		Matrix4x4 P = GetObjectPose (ren);
		P.SetColumn (3, _exone(pt));
		SetObjectPose (ren, P);
	}

	public static void SetObjectPosition(GameObject obj, Vector3 pt)
	{
		Renderer ren = obj.GetComponent<Renderer> ();
		SetObjectPosition (ren, pt);
	}

	public static void TranslateObjectPosition(Renderer ren, Vector3 v)
	{
		Matrix4x4 P = GetObjectPose (ren);
		Vector3 pt = P.GetColumn (3);
		P.SetColumn (3, _exone(pt + v));
		SetObjectPose (ren, P);
	}

	public static Matrix4x4 GetCameraPose(Renderer ren)
	{
		return ren.sharedMaterial.GetMatrix ("_camerapose");
	}

	public static Matrix4x4 GetCameraPose(GameObject obj)
	{
		Renderer ren = obj.GetComponent<Renderer>();
		return GetCameraPose(ren);
	}

	public static void SetCameraPose(Renderer ren, Matrix4x4 P)
	{
		ren.sharedMaterial.SetMatrix ("_camerapose",P);
	}

	public static void SetCameraPose(GameObject obj, Matrix4x4 P)
	{
		Renderer ren = obj.GetComponent<Renderer> ();
		SetCameraPose (ren, P);
	}

}