using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {
	public GameObject tableObject;

	public struct Card {
		public int stats, startStats;
		public GameObject cardObject, statsObject;

		public Card(int _stats, GameObject _cardObject, GameObject _statsObject) {
			stats = _stats;
			startStats = _stats;
			cardObject = _cardObject;
			statsObject = _statsObject;
		}

		public void UpdateCard() {
			statsObject.GetComponent<UnityEngine.UI.Text> ().text = "" + stats;
			if (stats > startStats)
				statsObject.GetComponent<UnityEngine.UI.Text> ().color = new Color (0, 60, 0);
			if(stats < startStats)
				statsObject.GetComponent<UnityEngine.UI.Text> ().color = new Color (170, 0, 0);
			if (stats == startStats)
				statsObject.GetComponent<UnityEngine.UI.Text> ().color = Color.black;
		}

		public void CardDie() {
			stats = 0;
			Destroy (cardObject);
		}

		public Vector3 GetPosition (float cardAmount, float number, GameObject fieldObj) {
			float widthCard = cardObject.GetComponent<RectTransform> ().sizeDelta.x + 5;
			float widthCards = widthCard * cardAmount;
			float midPointX = fieldObj.GetComponent<RectTransform>().position.x - widthCards/2 + 
				(number-0.5f) * widthCard;
			float midPointY = fieldObj.transform.position.y + cardObject.GetComponent<RectTransform> ().sizeDelta.y/2;

			return new Vector3(midPointX, midPointY, 0);
		}
	};

	public struct CardList {
		#region Variables
		public Card[] cards;
		public int currSize;
		public GameObject TableObject;
		#endregion

		public void UpdatePosition() {
			for (int i = 0; i < currSize; ++i) {
				cards [i].cardObject.transform.position = cards [i].GetPosition (currSize, i+1, TableObject);
			}
		}

		public void UpdateStats() {
			for (int i = 0; i < currSize; ++i) {
				cards [i].UpdateCard ();

				if (cards [i].stats <= 0) {
					RemoveCard (i);
				}
			}
		}

		public CardList (int startSize, GameObject _tableObject) {
			cards = new Card[9];
			currSize = 0;
			TableObject = _tableObject;
		}

		public void AddCard(Card card, int PlaceMode) {
			cards [currSize] = card;
			++currSize;
			if (PlaceMode == 0)
				UpdatePosition ();
		}

		public void PowerUp (int value) {
			for (int i = 0; i < currSize; ++i) 
				cards[i].stats += value;

			UpdateStats ();
		}

		public void CardSwap (int position1, int position2) {
			int tempStat = cards[position1].stats;
			int tempStartStat = cards [position1].startStats;
			GameObject tempObjStat = cards[position1].statsObject;
			GameObject tempObjCard = cards[position1].cardObject;

			cards[position1].stats = cards[position2].stats; cards[position1].cardObject = cards[position2].cardObject; cards[position1].statsObject = cards[position2].statsObject;
			cards[position2].stats = tempStat; cards[position2].cardObject = tempObjCard; cards[position2].statsObject = tempObjStat;
			cards [position1].startStats = cards [position2].startStats; cards [position2].startStats = tempStartStat;
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
		/*if (Input.GetKeyDown (KeyCode.G)) {
			TableList.PowerUp (2);
		}
		if (Input.GetKeyDown (KeyCode.H)) {
			TableList.PowerUp (-2);
		}*/
	}
}
