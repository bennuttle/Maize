using UnityEngine;
using System.Collections;

public class BttnController : MonoBehaviour {
	public Texture bg;
	public Texture exp;
	public Texture marks;
	public Texture shop;
	public GUISkin gSkin;
	private float midWidth = Screen.width/2;
	private float midHeight = Screen.height/2;
	private float buttonHeight = Screen.height * .1f;
	private float expWidth = Screen.width*.75f;
	private float markWidth = Screen.width*.6f;
	private float shopWidth = Screen.width*.45f;

	//create button size as ratio of original image size. 

	// Use this for background
	void OnGUI (){
		GUI.skin = gSkin;
		//GUI.DrawTexture (new Rect (0, 0, 1080, 1920), bg, ScaleMode.StretchToFill);
//		GUI.Box(new Rect(0,0,700,1244), bg);

		if(GUI.Button (new Rect(Screen.width * 0.25f, buttonHeight * 1.5f,expWidth,buttonHeight), exp)) {
			//load level
			Application.LoadLevel(1);
		}
		if(GUI.Button (new Rect(Screen.width * 0.4f,buttonHeight * 2.5f,markWidth,buttonHeight), marks)) {
			
		}
		if(GUI.Button (new Rect(Screen.width * 0.55f, buttonHeight * 3.5f,shopWidth,buttonHeight), shop)) {
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit();
		}
	}
}
