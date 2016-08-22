using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public GameObject credits;
	public GameObject menu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			credits.SetActive (false);
			menu.SetActive (true);

		}
	}

	public void showCredit() {
		credits.SetActive (true);
		menu.SetActive (false);
	}

	public void startGame() {
		Application.LoadLevel (1);

	}
}
