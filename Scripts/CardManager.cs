﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {
	public struct Card {
		public int stats;
		public GameObject cardObject, statsObject;

		public Card(int _stats, GameObject _cardObject, GameObject _statsObject) {
			stats = _stats;
			cardObject = _cardObject;
			statsObject = _statsObject;
		}

		public void UpdateCard() {
			statsObject.GetComponent<UnityEngine.UI.Text> ().text = "" + stats;
		}

		public void CardDie() {
			stats = 0;
			//Destroy (cardObject);
		}
	};

	public struct CardList {
		public Card[] cards;
		public int currSize;

		public CardList (int startSize) {
			currSize = startSize;
			cards = new Card[9];
		}

		public void AddCard(Card card) {
			cards [currSize] = card;
			++currSize;
		}

		public void CardSwap (int position1, int position2) {
			int tempStat = cards[position1].stats;
			GameObject tempObjStat = cards[position1].statsObject;
			GameObject tempObjCard = cards[position1].cardObject;

			cards[position1].stats = cards[position2].stats; cards[position1].cardObject = cards[position2].cardObject; cards[position1].statsObject = cards[position2].statsObject;
			cards[position2].stats = tempStat; cards[position2].cardObject = tempObjCard; cards[position2].statsObject = tempObjStat;
		}

		public void RemoveCard (int delPosition) {
			cards [delPosition].CardDie ();
			for (int i = delPosition + 1; i < currSize; ++i) {
				CardSwap (i - 1, i);
			}
			--currSize;
		}

		public void printInfo() {
			for (int i = 0; i < currSize; ++i) 
				Debug.Log ("" + cards[i].stats);
		}

		public int AllStats () {
			int res = 0;
			foreach (Card card in cards) {
				res += card.stats;
			}

			return res;
		}
	};

	public CardList cardList = new CardList(0);

	public GameObject ScoreObject;

	public void Start() {
		cardList.AddCard (new Card(9, ScoreObject, ScoreObject));
		cardList.AddCard (new Card(8, ScoreObject, ScoreObject));
		cardList.AddCard (new Card(27, ScoreObject, ScoreObject));
		cardList.RemoveCard (1);
		cardList.AddCard (new Card (228, ScoreObject, ScoreObject));
		cardList.RemoveCard (0);
		cardList.RemoveCard (1);
	}

	public void Update() {
		
		if (cardList.AllStats() > 0)
			ScoreObject.GetComponent<UnityEngine.UI.Text> ().text = "" + cardList.AllStats(); 
		else ScoreObject.GetComponent<UnityEngine.UI.Text> ().text = "0";

		if (Input.GetKeyDown (KeyCode.F)) {
			cardList.printInfo ();
		}
	}
}
