using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointInvKinematics : MonoBehaviour {
	public HingeJoint[] joints;
	public GameObject target;

	protected event TriggerEvent OnRobotArmAction;

	void Start(){
		RobotArmAudio robotAudioController = this.gameObject.GetComponent<RobotArmAudio> ();
		if (robotAudioController != null)
			OnRobotArmAction += new TriggerEvent (robotAudioController.Handler);
	}

	static float OddErp(float x, float scale){
		return Mathf.Sign (x) * (1f - Mathf.Cos (x / scale * Mathf.PI * 0.5f)) * scale;
	}

	Vector3 Follow(HingeJoint joint, Vector3 target){
		Vector3 a = Vector3.up, b = joint.connectedBody.transform.InverseTransformPoint(target);
		float err = Vector3.Angle (a, b) * Mathf.Sign (Vector3.Cross(a, b).x);
//		print (string.Format("a:{0} b:{1}, err:{2}", a, b, err));
		this.OnRobotArmAction(gameObject, err);

		JointSpring s = joint.spring;
		s.targetPosition -= Mathf.Rad2Deg * OddErp (Mathf.Deg2Rad * 0.75f * err, Mathf.PI);
		joint.spring = s;

		ArmMetadata connectedMetadata = joint.connectedBody.GetComponent<ArmMetadata> ();
		if (connectedMetadata == null) throw new MissingComponentException ("ArmMetadata not found");
		return target - joint.connectedBody.transform.TransformVector(b.normalized * connectedMetadata.Length);
	}

	void Update () {
		if (target == null) return;
		Vector3 p = target.transform.position;
		for (int i = joints.Length - 1; i >= 0; --i)
			p = Follow (joints [i], p);
	}
}
