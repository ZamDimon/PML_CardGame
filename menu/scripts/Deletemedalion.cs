﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deletemedalion : MonoBehaviour {
	private bool IsActive;
	void Update () {
		if (this.gameObject.GetComponent<Image> ().color.a == 0) {
			Destroy (transform.parent.gameObject);
			Destroy (this.gameObject);
		}
	}
	public void ClickOn(){
		this.gameObject.GetComponent<Animator> ().SetBool ("Ani", true);
	}
}