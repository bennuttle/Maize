using UnityEngine;
using System.Collections;

public class TimeGUI : MonoBehaviour
{
	float milliseconds = 0;
	int seconds = 0;
	int minutes = 0;
	string displayTime;

	void Update()
	{
		GameObject timeRef = this.gameObject;
		//		GameObject timeRef = GameObject.Find("Character_2_withTime");
		PlayerMotion timeRefScript = (PlayerMotion) timeRef.GetComponent(typeof(PlayerMotion));
		if(!timeRefScript.getIsDone()) {
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
		GUI.Box(new Rect(Screen.width * 0.45f, Screen.height * 0.95f, Screen.width * 0.1f, Screen.height * 0.05f),displayTime);
	}

}
