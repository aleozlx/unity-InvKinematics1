using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakyPlatform : MonoBehaviour {
//	protected Vector3 offset = new Vector3(), pos;
	// Use this for initialization
	void Start () {
//		Vector3 pos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		var t = Time.time * 0.1f;
		gameObject.transform.rotation = Quaternion.Euler (Mathf.PerlinNoise (t, 0) * 60f - 25f, 0, 0);
//		offset.Set (0f, Mathf.PerlinNoise (t, 0.5f), 0f);
//		gameObject.transform.position = pos + offset;
	}
}
