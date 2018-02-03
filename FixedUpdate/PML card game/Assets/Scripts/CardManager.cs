using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {
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
				statsObject.GetComponent<UnityEngine.UI.Text> ().color = Color.white;
		}

		public void CardDie() {
			stats = 0;
			Destroy (cardObject);
		}

		public Vector3 GetPosition (float cardAmount, float number, GameObject fieldObj, int player) {
			float widthCard = cardObject.GetComponent<RectTransform> ().sizeDelta.x + 5;
			float widthCards = widthCard * cardAmount;
			float midPointX = fieldObj.GetComponent<RectTransform>().position.x - widthCards/2 + 
				(number-0.5f) * widthCard;
			
			float midPointY = 0f;

			if(player == 0)
				midPointY = fieldObj.transform.position.y +
					Mathf.Abs(fieldObj.GetComponent<RectTransform> ().sizeDelta.y - cardObject.GetComponent<RectTransform> ().sizeDelta.y)/2;
			else midPointY = fieldObj.transform.position.y - cardObject.GetComponent<RectTransform> ().sizeDelta.y - 
					Mathf.Abs(fieldObj.GetComponent<RectTransform> ().sizeDelta.y - cardObject.GetComponent<RectTransform> ().sizeDelta.y)/2;

			return new Vector3(midPointX, midPointY, 0);
		}
	};

	public struct CardList {
		#region Variables
		public Card[] cards;
		public int currSize;
		public GameObject TableObject;
		public int player;
		#endregion

		public void UpdatePosition() {
			for (int i = 0; i < currSize; ++i) {
				cards [i].cardObject.transform.position = cards [i].GetPosition (currSize, i+1, TableObject, player);
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

		public CardList (int startSize, GameObject _tableObject, int _player) {
			cards = new Card[9];
			currSize = 0;
			TableObject = _tableObject;
			player = _player;
		}

		public void AddCard(Card card, int PlaceMode) {
			cards [currSize] = card;
			++currSize;
			if (PlaceMode == 0)
				UpdatePosition ();
		}

		public void QuickAddCard (Card card) {
			GameObject createdCard = Instantiate (card.cardObject, new Vector3 (0, 0, 0), Quaternion.identity);
			createdCard.GetComponent<PlaceCard> ().enabled = false;
			card.cardObject = createdCard;
			card.statsObject = createdCard.transform.Find ("Stats").gameObject;
			cards [currSize] = card;
			++currSize;
			createdCard.transform.SetParent (GameObject.FindGameObjectWithTag ("CardManager").transform); 
			UpdatePosition (); UpdateStats ();
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

	#region Lists
	public CardList TableList;
	public CardList DeckList;
	public CardList handList;
	public CardList dieList; 

	public CardList TableList_2;
	public CardList DeckList_2;
	public CardList handList_2;
	public CardList dieList_2; 
	#endregion

	#region Objects
	public GameObject ScoreObject;
	public GameObject ScoreObject2;

	public GameObject tableObject;
	public GameObject tableObject2;
	#endregion

	public void Start() {
		TableList = new CardList (0, tableObject, 0);
		DeckList = new CardList (0, tableObject, 0);
		handList = new CardList (0, tableObject, 0);
		dieList = new CardList (0, tableObject, 0);

		TableList_2 = new CardList (0, tableObject2, 1);
		DeckList_2 = new CardList (0, tableObject2, 1);
		handList_2 = new CardList (0, tableObject2, 1);
		dieList_2 = new CardList (0, tableObject2, 1);
	}

	public void Update() {
		ScoreObject.GetComponent<UnityEngine.UI.Text> ().text = "" + TableList.AllStats ();
		ScoreObject2.GetComponent<UnityEngine.UI.Text> ().text = "" + TableList_2.AllStats ();
		if (Input.GetKeyDown (KeyCode.G)) {
			TableList.PowerUp (2);
		}
		if (Input.GetKeyDown (KeyCode.H)) {
			TableList.PowerUp (-2);
		}
	}
}
