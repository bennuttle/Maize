using UnityEngine;
using System.Collections;

public class TimeGUI : MonoBehaviour
{
	float milliseconds = 0;
	int seconds = 0;
	int minutes = 0;
	string displayTime;
	private bool paused = false;


	void Update()
	{
		GameObject timeRef = GameObject.Find("Character_2_withTime");
		StepMotion timeRefScript = (StepMotion) timeRef.GetComponent(typeof(StepMotion));
		if(!timeRefScript.getIsDone() && !paused) {
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
		displayTime = minutes.ToString ("00") + ":" + seconds.ToString ("00") + ":" + milliseconds.ToString (".00").Replace (".", "");
		GUI.Box(new Rect(Screen.width * 0.4f, Screen.height * 0.9f, Screen.width * 0.2f, Screen.height * 0.1f),"<size=40>"+displayTime+"</size>");
		if( GUI.Button(new Rect( Screen.width * 0.9f, 0, Screen.width * 0.1f, Screen.height * 0.2f),"<size=40>P</size>"))
		{ 
			pause ();
		}
		if (paused) {
			GUI.Box(new Rect(Screen.width * 0.35f, Screen.height * 0.35f, Screen.width * 0.3f, Screen.height * 0.15f),"<size=40>Paused</size>");
			if( GUI.Button(new Rect(Screen.width * 0.35f, Screen.height * 0.5f, Screen.width * 0.3f, Screen.height * 0.15f),"<size=40>reset</size>"))
			{ 
				Application.LoadLevel (Application.loadedLevel);
			}
			if( GUI.Button(new Rect(Screen.width * 0.35f, Screen.height * 0.65f, Screen.width * 0.3f, Screen.height * 0.15f),"<size=40>Quit</size>"))
			{ 
				Application.Quit();
			}
		}
	}
	public void pause() {
		if(paused) {
			paused = false;
		} else {
			paused = true;
		}
	}
	public bool getPause() {
		return paused;
	}
}
