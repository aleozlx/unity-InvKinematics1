using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseKinematics : MonoBehaviour {
	public GameObject ball, test;
	public GameObject arm1, arm2;
	public HingeJoint joint1, joint2;

	void Start () {
		ball = GameObject.Find ("Ball");
		arm1 = GameObject.Find ("Arm1");
		arm2 = GameObject.Find ("Arm2");
		test = GameObject.Find ("Test");
		joint1 = GetComponent<HingeJoint>();
		joint2 = arm1.GetComponent<HingeJoint> ();
	}

	void Update () {
		var anchor = arm1.transform.TransformPoint(joint2.anchor);
		var a = arm2.transform.position - anchor;
		var b = ball.transform.position - anchor;
		// test.transform.position = arm2.transform.position;
		var x = arm2.transform.InverseTransformPoint (Vector3.Cross (a, b)).x;
		var e2 = Vector3.Angle (a, b) * Mathf.Sign (x);

		// print (string.Format("{0} {1}", e2, joint2.spring.targetPosition));

		JointSpring hingeSpring = joint2.spring;
		hingeSpring.targetPosition -= 0.2f * e2;//0.02f * Mathf.Pow(e2, 3);
		joint2.spring = hingeSpring;
	}
}
