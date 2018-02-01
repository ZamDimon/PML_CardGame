using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudoA : MonoBehaviour {
	public AudioClip Clip;
	public GameObject Cam;
	public void OnClick(){
		AudioSource.PlayClipAtPoint (Clip, Cam.transform.position);
	}
}
