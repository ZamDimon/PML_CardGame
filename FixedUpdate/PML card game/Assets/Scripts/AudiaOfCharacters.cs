using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiaOfCharacters : MonoBehaviour {
	public GameObject Ubludki, Horosho, Spasibo;// clips for ciri
	private bool IsOpen,IsTimerActive;
	public float Delay;
	private float Timer;
	void Update(){
		if (IsTimerActive) {
			Timer += Time.deltaTime;
		}
		if (Timer >= Delay) {
			Ubludki.SetActive (false);
			Horosho.SetActive (false);
			Spasibo.SetActive (false);
			IsOpen = false;
			IsTimerActive = false;
			Timer = 0;
		}
		if (IsOpen && Input.GetMouseButton(0)) {
			Ubludki.GetComponent<Animator> ().SetBool ("Close",true);
			Horosho.GetComponent<Animator> ().SetBool ("Close",true);
			Spasibo.GetComponent<Animator> ().SetBool ("Close",true);
			IsTimerActive = true;
		}
	}
	public void ClickOnChar () {
		if (!IsOpen) { //Open dialog menu
			Ubludki.SetActive (true);
			Horosho.SetActive (true);
			Spasibo.SetActive (true);
			IsOpen = true;
		}
	}

}
