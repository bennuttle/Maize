using UnityEngine;
using System.Collections;

public class TimeGUI : MonoBehaviour
{
	float milliseconds = 0;
	int seconds = 0;
	int minutes = 0;
	string displayTime;
	PlayerMotion timeRefScript;
	public GUISkin customSkin;
//	var menuSkin : GUISkin;

	void Update()
	{
		GameObject timeRef = this.gameObject;
		//		GameObject timeRef = GameObject.Find("Character_2_withTime");
		timeRefScript = (PlayerMotion) timeRef.GetComponent(typeof(PlayerMotion));
		if(!timeRefScript.getIsDone() && !timeRefScript.getIsPaused()) {
			milliseconds += Time.deltaTime;
			if (milliseconds > 1)
			{
				seconds++;
				milliseconds = 0;
			}
			if (seconds > 60)
			{
				minutes++;
				seconds = 0;
			}
		}
	}
	void OnGUI()
	{
		GUI.skin = customSkin;
//		GUILayout.Button ("I am a custom styled Button", "button3");
		displayTime = minutes.ToString ("00") + ":" + seconds.ToString ("00") + ":" + milliseconds.ToString (".00").Replace (".", "");
		GUI.Box(new Rect(Screen.width * 0.35f, Screen.height * 0.9f, Screen.width * 0.3f, Screen.height * 0.1f),"<size=40>" + displayTime + "</size>", "time");

		if(timeRefScript.getIsPaused() && !timeRefScript.getIsDone()) {
//			GUI.Window(new Rect(Screen.width * 0.25f,Screen.height * 0.25f,Screen.width * 0.5f,Screen.height * 0.5f));
			GUI.BeginGroup (new Rect(Screen.width * 0.25f,Screen.height * 0.25f,Screen.width * 0.5f,Screen.height * 0.6f));
			if(GUI.Button (new Rect(0,0,Screen.width * 0.5f,Screen.height * 0.15f), "Resume","button1")) {
				timeRefScript.unPause();
			}
			if(GUI.Button (new Rect(0,Screen.height * 0.2f,Screen.width * 0.5f,Screen.height * 0.15f), "Restart Maze","button2")) {
				Application.LoadLevel(1);
			}
			if(GUI.Button (new Rect(0,Screen.height * 0.4f,Screen.width * 0.5f,Screen.height * 0.15f), "Main Menu","button3")) {
				Application.LoadLevel(0);
			}
			
			GUI.EndGroup ();
		}

		if(timeRefScript.getIsDone()) {
			GUI.BeginGroup (new Rect(Screen.width * 0.25f,Screen.height * 0.25f,Screen.width * 0.5f,Screen.height * 0.6f));
			GUI.Box(new Rect(0,0,Screen.width * 0.5f,Screen.height * 0.15f), "You Won! Time: "+ displayTime, "button1");
			if(GUI.Button (new Rect(0,Screen.height * 0.2f,Screen.width * 0.5f,Screen.height * 0.15f), "Restart Maze", "button2")) {
				Application.LoadLevel(1);
			}
			if(GUI.Button (new Rect(0,Screen.height * 0.4f,Screen.width * 0.5f,Screen.height * 0.15f), "Main Menu", "button3")) {
				Application.LoadLevel(0);
			}

			GUI.EndGroup ();
		}
	}

}
