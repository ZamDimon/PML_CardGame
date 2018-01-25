using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlaceCard : MonoBehaviour {

	private Vector3 pointToMove; //Point, where card will be moving
	private Vector3 originPosition; //Start position of a card
	private Vector3 OnCardPosition; //Position of a card when the mouse is on
	public float animationSpeed; //Speed of animation

	public void Start() {
		//Setting start values
		originPosition = transform.position; 
		OnCardPosition = transform.position + new Vector3 (0, 40, 0); 
		pointToMove = originPosition; 
	}

	private void CardAnimation(List <RaycastResult> rayResults) { //Animation of cards method
		if (rayResults.Count > 0) { 
			//When we have an array of raycast objects, we need to check if one of them is our object
			int i = 0; bool FoundCard = false;
			while (i < rayResults.Count) {
				if (rayResults [i].gameObject.transform == transform) {
					FoundCard = true;
					break; //If we found an object, just stop while operation and set FoundCard = true.
				}

				++i;
			}

			pointToMove = FoundCard? OnCardPosition : originPosition;   //If we found a card, setting new value for pointToMove, else setting an origin position
		} 
	}
		
	public void Update() {
		//Moving card to a pointToMove position
		transform.position = Vector3.Lerp(transform.position, pointToMove, Time.deltaTime * animationSpeed); 

		#region UIRaycasts //Raycast for UI
		PointerEventData pointerData = new PointerEventData (EventSystem.current);
		pointerData.position = Input.mousePosition;
		List<RaycastResult> results = new List<RaycastResult> ();
		EventSystem.current.RaycastAll (pointerData, results);
		#endregion

		CardAnimation (results); //Play animation
	}
}
