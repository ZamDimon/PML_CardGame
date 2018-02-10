using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {
	private Slider SliderVol;

	void Start () {
		SliderVol = GetComponent<Slider> ();
	}
	void Update () {
		PlayerPrefs.SetFloat ("Volume", SliderVol.value);
	}
}
