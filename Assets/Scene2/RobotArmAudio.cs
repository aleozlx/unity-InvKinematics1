using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TriggerEvent(GameObject sender, float error);

[RequireComponent(typeof(AudioSource))]
public class RobotArmAudio : MonoBehaviour {
	public AudioClip start, moving, end;
	public float threshold = 10f;
	protected AudioSource audio;

	enum State{ Start, Moving, End };
	State state = State.End;

	void Start () {
		audio = this.gameObject.GetComponent<AudioSource> ();
	}

	public void Handler (GameObject sender, float err){
		switch (state) {
		case State.Start:
			if (err > threshold) {
				if (!audio.isPlaying) {
					audio.PlayOneShot (moving);
					state = State.Moving;
				}
			} else {
				if (!audio.isPlaying) {
					audio.PlayOneShot (end);
					state = State.End;
				}
			}
			break;
		case State.Moving:
			if (err > threshold) {
				if (!audio.isPlaying) {
					audio.PlayOneShot (moving);
				}
			} else {
				if (!audio.isPlaying) {
					audio.PlayOneShot (end);
					state = State.End;
				}
			}
			break;
		case State.End:
			if (err > threshold) {
				if (!audio.isPlaying) {
					audio.PlayOneShot (start);
					state = State.Start;
				}
			}
			break;
		}
	}
}
