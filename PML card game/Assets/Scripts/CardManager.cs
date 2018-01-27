using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {
	public GameObject tableObject;

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
			Destroy (cardObject);
		}

		public Vector3 GetPosition (float cardAmount, float number, GameObject fieldObj) {
			float width = (cardObject.GetComponent<RectTransform> ().sizeDelta.x + 5) * cardAmount;
			float midPointX = fieldObj.transform.position.x - width / 2 + 
				number * (cardObject.GetComponent<RectTransform> ().sizeDelta.x + 5);
			float midPointY = fieldObj.transform.position.y + cardObject.GetComponent<RectTransform>().sizeDelta.y/2;

			return new Vector3(midPointX, midPointY, 0);
		}
	};

	public struct CardList {
		public Card[] cards;
		public int currSize;
		public GameObject TableObject;

		public void UpdatePosition() {
			for (int i = 0; i < currSize; ++i) {
				cards [i].cardObject.transform.position = cards [i].GetPosition (currSize, i+1, TableObject);
			}
		}

		public CardList (int startSize, GameObject _tableObject) {
			cards = new Card[9];
			currSize = 0;
			TableObject = _tableObject;
		}

		public void AddCard(Card card) {
			cards [currSize] = card;
			++currSize;
			UpdatePosition ();
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

			UpdatePosition ();
		}

		public void printInfo() {
			Debug.Log ("Size:" + currSize + "; Elements:");
			for (int i = 0; i < currSize; ++i) 
				Debug.Log ("" + cards[i].stats);
		}

		public int AllStats () {
			int res = 0;

			for (int i = 0; i < currSize; ++i)
				res += cards [i].stats;

			return res;
		}
	};

	public CardList TableList;
	public CardList DeckList;
	public CardList handList;
	public CardList dieList; 

	public GameObject ScoreObject;

	public void Start() {
		TableList = new CardList (0, tableObject);
		DeckList = new CardList (0, tableObject);
		handList = new CardList (0, tableObject);
		dieList = new CardList (0, tableObject);
	}

	public void Update() {
		ScoreObject.GetComponent<UnityEngine.UI.Text> ().text = "" + TableList.AllStats ();
		if (Input.GetKeyDown (KeyCode.G))
			TableList.RemoveCard (0);
	}
}
