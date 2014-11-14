using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {
	public GUISkin customSkin;
	public Texture2D logo;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI() {
		GUI.skin = customSkin;

		if(GUI.Button (new Rect(Screen.width * 0.3f,Screen.height * 0.15f,Screen.width * 0.7f,Screen.height * 0.1f), "Explore","button1")) {
			Application.LoadLevel(1);
		}

		if(GUI.Button (new Rect(Screen.width * 0.4f,Screen.height * 0.25f,Screen.width * 0.6f,Screen.height * 0.1f), "Tutorial","button2")) {
//			Application.LoadLevel(2);
		}
		
		if(GUI.Button (new Rect(Screen.width * 0.5f,Screen.height * 0.35f,Screen.width * 0.5f,Screen.height * 0.1f), "Marks","button3")) {
//			Application.LoadLevel(3);
		}

		GUI.Box (new Rect (Screen.width * 0.08f, Screen.height * 0.85f, Screen.width * 0.2f, Screen.height * 0.1f), logo);
	}
}
