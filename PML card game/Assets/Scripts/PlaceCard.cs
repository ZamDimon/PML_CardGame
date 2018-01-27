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
	public float moveSpeed; //Chasing the mouse speed
	private float realSpeed; //Current speed of a card (animationSpeed or moveSpeed)
	private int mode = 0;
	public float distanceToSet; //Distance, from which card will be placed
	private GameObject cardManager;
	public GameObject tableObject; //Table object

	[Space]
	public int StartStats;
	public GameObject StatsObject;

	public void Start() {
		//Setting start values
		originPosition = transform.position; 
		OnCardPosition = transform.position + new Vector3 (0, 40, 0); 
		pointToMove = originPosition; 
		realSpeed = animationSpeed;
		cardManager = GameObject.FindGameObjectWithTag ("CardManager");
	}

	Vector3 PlacingAnimationPosition() { //Position of a card when player play it
		float yPosition = Screen.height / 2 - this.GetComponent<RectTransform> ().sizeDelta.y / 2;
		float xPosition = Screen.width - this.GetComponent<RectTransform> ().sizeDelta.x;
		return new Vector3 (xPosition, yPosition, 0);
	}

	bool IsThisCard(List <RaycastResult> rayResults) { //"Did we find this card" method
		if (rayResults.Count < 0) return false;

		//When we have an array of raycast objects, we need to check if one of them is our object
		int correctResults = 0; bool FoundThisCard = false;
		foreach (RaycastResult result in rayResults) {
			if (result.gameObject.transform.tag == "Card")
				++correctResults;
			if (result.gameObject.transform == transform)
				FoundThisCard = true;
		}

		if (correctResults == 1 && FoundThisCard) 
			return true;
		
		return false;
	} 

	bool CanChooseCard () {
		GameObject[] _cards = GameObject.FindGameObjectsWithTag ("Card");
		for (int i = 0; i < _cards.Length; ++i) {
			if (_cards[i].GetComponent<PlaceCard> ().mode != 0)
				return false;
		}

		return true;
	}

	Vector3 MousePoint () {
		float positionX = Input.mousePosition.x;
		float positionY = Input.mousePosition.y - transform.GetComponent<RectTransform> ().sizeDelta.y / 2;
		return new Vector3 (positionX, positionY, 0);
	}
		
	public void FixedUpdate() {
		//Moving card to a pointToMove position
		transform.position = Vector3.Lerp(transform.position, pointToMove, Time.deltaTime * realSpeed); 

		switch (mode) {
		case 0:
			//If we didn't choose the card
			#region UIRaycasts //Raycast for UI
			PointerEventData pointerData = new PointerEventData (EventSystem.current);
			pointerData.position = Input.mousePosition;
			List<RaycastResult> results = new List<RaycastResult> ();
			EventSystem.current.RaycastAll (pointerData, results);
			#endregion
			
			pointToMove = (IsThisCard (results) && !Input.GetMouseButton (0)) ? OnCardPosition : originPosition; //Playing animation
			realSpeed = (IsThisCard (results) && Input.GetMouseButton (0)) ? moveSpeed : realSpeed;

			if (IsThisCard (results) && Input.GetMouseButton (0) && CanChooseCard()) 
				mode = 1;
			
			break;

		case 1: 
			//If we choose the card
			pointToMove = MousePoint ();
			if (Input.GetMouseButtonUp (0)) 
				mode = (Input.mousePosition.y < distanceToSet) ? 0 : 2;
			
			break;

		case 2:
			pointToMove = PlacingAnimationPosition ();
			if (Vector3.Distance(pointToMove, transform.position) < 1f) {
				CardManager.Card card = new CardManager.Card (StartStats, this.gameObject, StatsObject);
				cardManager.GetComponent<CardManager> ().TableList.AddCard (card);
				pointToMove = card.GetPosition (cardManager.GetComponent<CardManager> ().TableList.currSize, cardManager.GetComponent<CardManager> ().TableList.currSize, tableObject);
				mode = 3;
			}
			break;

		case 3:
			if (Vector3.Distance (pointToMove, transform.position) < 1f) {
				mode = 0;
				this.GetComponent<PlaceCard> ().enabled = false;
			}
			break;
		}
	}
}
