using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntersectPanels : MonoBehaviour {

	public Transform obj1;
	public Transform obj2;

	void Start () {
		float distance = Vector2.Distance (new Vector2(obj1.position.x, obj1.position.y), new Vector2(obj2.position.x, obj2.position.y));
		RectTransform RT1 = obj1.GetComponent<RectTransform> ();
		RectTransform RT2 = obj2.GetComponent<RectTransform> ();

		float realDistance = distance - RT1.sizeDelta.y - RT2.sizeDelta.y;
		RT1.sizeDelta = new Vector2 (RT1.sizeDelta.x, RT1.sizeDelta.y + realDistance / 2 - 5);
		RT2.sizeDelta = new Vector2(RT2.sizeDelta.x, RT2.sizeDelta.y + realDistance / 2 - 5);
	}
}
