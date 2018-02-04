using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AllMenuButtons : MonoBehaviour {
	public GameObject SlidePref, Menu, MenuPoint, SlidePoint, TrashPoint, Slide, MedalionAni,SpawnPoint;
	public GameObject[] ActivatePanels;
	public AllMenuButtons Script;
	public bool GoToLeftMenu,GoLeftSlide;
	private bool IsTimerActive;
	private float Timer;
	public string Bar, BuyWhat, ReturnWhat;
	public int Cost;
	public Text Text;

	public void ExitGame(){
		Application.Quit ();
	}
	public void Play(){
		SceneManager.LoadScene ("GameScene");
	}
	public void SlideManagmentToSlide(){//Click on MenuButton
		if (SlidePref != null) {
			Slide = Instantiate (SlidePref, SlidePoint.transform.position, Quaternion.identity);
			Slide.transform.SetParent (GameObject.Find ("Canvas").transform);
			Slide.transform.position = SlidePoint.transform.position;
			Slide.GetComponentInChildren<AllMenuButtons>().Script = this.gameObject.GetComponent<AllMenuButtons>();
			GoToLeftMenu = true;
			GoLeftSlide = true;
		}
	}
	public void SlideManagmentToMenu(){// Click on Button "Back" on slide
		if (SlidePref != null) {
			IsTimerActive = true;
			Script.GoLeftSlide = false;
			Script.GoToLeftMenu = false;
		}
	}
	void Icon(){
		Text.text = PlayerPrefs.GetInt (ReturnWhat) + "";
	}
	public void ActivateMenu(){
		foreach (var ActivateMenu in ActivatePanels) {
			ActivateMenu.SetActive (!ActivateMenu.activeSelf);
		}
		if (!ActivatePanels [1].activeSelf) {
			this.gameObject.GetComponentInChildren<Text> ().text = "Купить медальоны";
		} else {
			this.gameObject.GetComponentInChildren<Text> ().text = "Открыть медальоны ";
		}
	}
	public void BuyMedalonInShop(){
		int Count = PlayerPrefs.GetInt (BuyWhat);
		if (PlayerPrefs.GetInt ("Money") >= Cost) {
			PlayerPrefs.SetInt ("Money", PlayerPrefs.GetInt ("Money") - Cost);
			PlayerPrefs.SetInt (BuyWhat, Count + 1);
		} else {
		}
	}
	public void OpenMedalion(){
		if (PlayerPrefs.GetInt (BuyWhat) > 0) {
			GameObject Medalion = Instantiate (MedalionAni,GameObject.Find("SpawnPoint").transform.position, Quaternion.identity);
			Medalion.transform.SetParent (GameObject.Find("Canvas").transform);
			Medalion.transform.position = GameObject.Find ("SpawnPoint").transform.position;
			Medalion.GetComponent<RectTransform> ().sizeDelta = new Vector2 (GameObject.Find ("Canvas").GetComponent<RectTransform> ().sizeDelta.x, GameObject.Find ("Canvas").GetComponent<RectTransform> ().sizeDelta.y);
			PlayerPrefs.SetInt (BuyWhat, PlayerPrefs.GetInt (BuyWhat) - 1);
		}
	}
	void Update(){
		if (Input.GetKey (KeyCode.M)) {
			PlayerPrefs.SetInt ("Money", PlayerPrefs.GetInt ("Money") + 10);
		} else if (Input.GetKey (KeyCode.N)) {
			PlayerPrefs.SetInt ("Money", PlayerPrefs.GetInt ("Money") - 10);
		}
		if (Input.GetKey (KeyCode.D)) {
			PlayerPrefs.DeleteAll ();
		}
		if (this.gameObject.tag == "Icon") {
			Icon ();
		}
		if (this.gameObject.tag == "Menu"&& Slide !=null) {
			if (GoLeftSlide) { //change menu and new slide positions
				Slide.transform.position = Vector3.Lerp (Slide.transform.position, MenuPoint.transform.position, Time.deltaTime * 4f);
			} else {
				Slide.transform.position = Vector3.Lerp (Slide.transform.position, SlidePoint.transform.transform.position, Time.deltaTime * 4f);
			}
			if (GoToLeftMenu) {
				Menu.transform.position = Vector3.Lerp (Menu.transform.position, TrashPoint.transform.position, Time.deltaTime * 4f);
			} else {
				Menu.transform.position = Vector3.Lerp (Menu.transform.position, MenuPoint.transform.position, Time.deltaTime * 4f);
			}
		}
		if (IsTimerActive){
			Timer += Time.deltaTime;
		}
		if (Timer>= 1) {//Destroy new SLide
			Destroy (Slide);
		}
	}
}
