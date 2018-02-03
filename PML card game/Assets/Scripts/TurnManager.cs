using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

	[HideInInspector]
	public int playerTurn = 0;
	public GameObject textObject;

	public GameObject AddedCard;
	public int cardValue;
	public GameObject AddedCardStats;

	public void OnaClick() {
		playerTurn = (playerTurn == 0) ? 1 : 0;
	}

	void SetLabel() {
		textObject.GetComponent<UnityEngine.UI.Text>().text = (playerTurn == 0)? "Your turn" : "Enemy's turn";
		textObject.GetComponent<UnityEngine.UI.Text>().color = (playerTurn == 0)? Color.green : Color.red;
	}

	void FixedUpdate() {
		SetLabel ();
		if (playerTurn == 1) {
			GameObject.FindGameObjectWithTag ("CardManager").GetComponent<CardManager> ().TableList_2.QuickAddCard
			(new CardManager.Card (cardValue, AddedCard, AddedCardStats));

			playerTurn = 0;
		}
	}
}
