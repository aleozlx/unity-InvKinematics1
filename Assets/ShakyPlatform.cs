using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakyPlatform : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var noise = Mathf.PerlinNoise (Time.time * 0.1f, 0);
		gameObject.transform.rotation = Quaternion.Euler (noise*60f-25f, 0, 0);
	}
}
