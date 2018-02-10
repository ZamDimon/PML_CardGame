using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour {
	public AudioClip[] MenuMusic;
	private AudioSource AudioSourse;
	private int NowTrack;
	void Start () {
		AudioSourse = GetComponent<AudioSource> ();
		NewTrack ();
	}
	public void NewTrack(){
		int Aud = UnityEngine.Random.Range (0, MenuMusic.Length);
		while (Aud == NowTrack) {
			Aud = UnityEngine.Random.Range (0, MenuMusic.Length);
		}
		Debug.Log ("Track number " + Aud);
		NowTrack = Aud;
		AudioSourse.clip =MenuMusic [Aud];
		AudioSourse.Play ();
	}
	void Update(){
		AudioSourse.volume = PlayerPrefs.GetFloat ("Volume") / 2;
		if (Input.GetKeyDown (KeyCode.R)) {
			NewTrack ();
		}
		if (!AudioSourse.isPlaying) {
			NewTrack ();
		}

	}
}
