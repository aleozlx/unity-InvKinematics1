using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointInvKinematics : MonoBehaviour {
	public HingeJoint[] joints;
	public GameObject baseObj;
	public GameObject target;

	void Start () {
		// target = GameObject.Find ("Ball");
	}

	static float OddErp(float x, float scale){
		return Mathf.Sign (x) * (1f - Mathf.Cos (x / scale * Mathf.PI * 0.5f)) * scale;
	}

//	static Vector3 GetConnectedArmCenter(GameObject robot_arm){
//		var connectedCenter = robot_arm.GetComponent<ArmMetadata>().ConnectBodyCenter;
//		return connectedCenter; //robot_arm.transform.TransformPoint(connectedCenter);
//	}

	Vector3 Follow(HingeJoint joint, Vector3 target){
		Vector3 a = Vector3.up, b = joint.connectedBody.transform.InverseTransformPoint(target);
		float err = Vector3.Angle (a, b) * Mathf.Sign (Vector3.Cross(a, b).x);
//		print (string.Format("a:{0} b:{1}, err:{2}", a, b, err));

		JointSpring s = joint.spring;
		s.targetPosition -= Mathf.Rad2Deg * OddErp (Mathf.Deg2Rad * 0.75f * err, Mathf.PI);
		joint.spring = s;

//		return target - (b.normalized * joint.connectedBody.transform.localScale.y);
		return new Vector3();
	}

	void Update () {
		Vector3 p = target.transform.position;
		for (int i = joints.Length - 1; i > 0; --i) {
			p = Follow (joints [i], p);
		}
	}
}
