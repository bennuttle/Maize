﻿using UnityEngine;
using System.Collections;

public class MarksController : MonoBehaviour {
	//public Texture bg;
	//public Texture exp;
	public Texture marks;
	public Texture achievements;
	public Texture marksRecs;
	//public Texture shop;
	public GUISkin gSkin;
	private float midWidth = Screen.width/2;
	private float midHeight = Screen.height/2;
	private float buttonHeight = Screen.height * .2f;
	//private float expWidth = Screen.width*.75f;
	private float markWidth = Screen.width*.6f;
	public Vector2 scrollPosition;
	public GUIStyle levelStyle;

	//create button size as ratio of original image size. 

	// Use this for background

	void OnGUI (){
		GUI.skin = gSkin;

		//draws header
		GUI.DrawTexture (new Rect(0,0,Screen.width,buttonHeight), marks);
		GUI.DrawTexture (new Rect(0,buttonHeight,Screen.width,(int)(buttonHeight/2)), marksRecs);
		GUI.Label(new Rect((int)(buttonHeight/2),(int)(buttonHeight*1.1),Screen.width,(int)(buttonHeight/2)),"L E V E L   17", levelStyle);
		scrollPosition = GUI.BeginScrollView(new Rect(0, buttonHeight+(int)(buttonHeight/2), Screen.width, Screen.height-buttonHeight), scrollPosition, 
		                                     new Rect(0, 0, Screen.width-30, buttonHeight*5),new GUIStyle(),new GUIStyle());
	

		GUILayout.BeginVertical();

		//marks box borders
		GUI.DrawTexture (new Rect(0,0,Screen.width,buttonHeight), marksRecs);
		GUI.DrawTexture (new Rect(0,buttonHeight,Screen.width,buttonHeight), marksRecs);
		GUI.DrawTexture (new Rect(0,buttonHeight*2,Screen.width,buttonHeight), marksRecs);
		GUI.DrawTexture (new Rect(0,buttonHeight*3,Screen.width,buttonHeight), marksRecs);
		GUI.DrawTexture (new Rect(0,buttonHeight*4,Screen.width,buttonHeight), marksRecs);


		//draw mark labels
		GUI.contentColor = Color.black;
		GUI.Label(new Rect(buttonHeight,(int)(buttonHeight*.5),Screen.width,(int)(buttonHeight/2)),"75 Mazes Completed");
		GUI.Label(new Rect(buttonHeight+6,(int)(buttonHeight*1.5),Screen.width,(int)(buttonHeight/2)),"1337 Steps Taken");
		GUI.Label(new Rect(buttonHeight+4,(int)(buttonHeight*2.5),Screen.width,(int)(buttonHeight/2)),"130 Mazes Explored");


		GUILayout.EndVertical();
		GUI.EndScrollView();

	//	GUI.DrawTexture (new Rect(0,Screen.height-(int)(buttonHeight/2),Screen.width,(int)(buttonHeight/2)), achievements);

		if(GUI.Button (new Rect(0,Screen.height-(int)(buttonHeight/2),Screen.width,(int)(buttonHeight/2)), achievements)) {
			Application.LoadLevel(4);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}