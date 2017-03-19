using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseKinematics : MonoBehaviour {
	public GameObject ball;
	public HingeJoint platformHindge;

	void Start () {
		ball = GameObject.Find("Ball");
		platformHindge = GetComponent<HingeJoint>();
	}

	void Update () {
		JointSpring hingeSpring = platformHindge.spring;
		hingeSpring.targetPosition = ball.transform.localPosition.y;
		platformHindge.spring = hingeSpring;
	}
}
