using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseKinematics : MonoBehaviour {
	public HingeJoint[] joints;
	public GameObject target;

	void Start () {
		// target = GameObject.Find ("Ball");
	}

	static Vector3 Follow(HingeJoint joint, Vector3 target){
		const float rate = 0.1f;
		var anchor = joint.gameObject.transform.TransformPoint (joint.anchor);
		var a = joint.connectedBody.transform.position - anchor;
		var b = target - anchor;
		var x = joint.connectedBody.transform.InverseTransformPoint (Vector3.Cross (a, b)).x;
		var err = Vector3.Angle (a, b) * Mathf.Sign (x);

		JointSpring s = joint.spring;
		s.targetPosition -= rate * err;
		joint.spring = s;

		return target - (b.normalized * joint.connectedBody.transform.localScale.y);
	}

	void Update () {
		Vector3 p = target.transform.position;
		for (int i = joints.Length - 1; i >= 0; --i) {
			p = Follow (joints [i], p);
		}
	}
}
