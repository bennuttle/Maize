using UnityEngine;
using System.Collections;

public class AchievementsController : MonoBehaviour {

	public Texture achievements;
	public Texture achievementsRecs;
	public Texture unlocked;

	public GUISkin gSkin;
	private float midWidth = Screen.width/2;
	private float midHeight = Screen.height/2;
	private float buttonHeight = Screen.height * .2f;
	//private float expWidth = Screen.width*.75f;
	private float markWidth = Screen.width*.6f;
	public Vector2 scrollPosition;
	string[] achievementList = new string[5] {"Complete A Maze", "Unlock Hard Difficulty", "Complete a 5x5x5", "test", "test2"};
	int[] unlockedList = new int[5] {1,0,0,1,0};
	float buttonCounter;
	float labelCounter = 0;
	float buttonCount = 5;
	//create button size as ratio of original image size. 
	
	//logic for loading in Player using Ben's Player Class
	//1. call LoadPlayer from class, save to local variable.
	//2. Get() attrs needed for menu screen.
	//3. Set() attrs to update stats after each maze (will happen in diff class probably)
	void OnGUI (){
		GUI.skin = gSkin;

		//draws header
		GUI.DrawTexture (new Rect(0,0,Screen.width,buttonHeight), achievements);
		scrollPosition = GUI.BeginScrollView(new Rect(0, buttonHeight, Screen.width, Screen.height-buttonHeight), scrollPosition, 
//		                                     new Rect(0, 0, Screen.width-30, buttonHeight*buttonCount));
											new Rect(0, 0, Screen.width-30, buttonHeight*5),new GUIStyle(),new GUIStyle());
		GUILayout.BeginVertical();
		buttonCounter = 0;
		GUI.contentColor = Color.black;
		print(unlockedList.Length);
		//achievements box borders
		for(int i = 0; i < unlockedList.Length;i++) {

			if(unlockedList[i]==1){
				GUI.DrawTexture (new Rect(0,buttonHeight*buttonCounter,Screen.width,buttonHeight), unlocked);
			}

			else{
				GUI.DrawTexture (new Rect(0,buttonHeight * buttonCounter,Screen.width,buttonHeight), achievementsRecs);
			}

			GUI.Label(new Rect(buttonHeight, (int)(buttonHeight*(buttonCounter+.5)),Screen.width,(int)(buttonHeight/2)),achievementList[i]);
			buttonCounter++;
		}

//		GUI.DrawTexture (new Rect(0,buttonCounter*buttonHeight,Screen.width,buttonHeight), unlocked);
//		GUI.DrawTexture (new Rect(0,buttonHeight,Screen.width,buttonHeight), achievementsRecs);
//		GUI.DrawTexture (new Rect(0,buttonHeight*2,Screen.width,buttonHeight), achievementsRecs);
//		GUI.DrawTexture (new Rect(0,buttonHeight*3,Screen.width,buttonHeight), achievementsRecs);
//		GUI.DrawTexture (new Rect(0,buttonHeight*4,Screen.width,buttonHeight), achievementsRecs);
//
//		//draw mark labels
//		GUI.contentColor = Color.black;
//		GUI.Label(new Rect(buttonHeight,(int)(buttonHeight*.5),Screen.width,(int)(buttonHeight/2)),"Complete 10 Mazes");
//		GUI.Label(new Rect(buttonHeight+6,(int)(buttonHeight*1.5),Screen.width,(int)(buttonHeight/2)),"Complete a 5x5x5");
//		GUI.Label(new Rect(buttonHeight+4,(int)(buttonHeight*2.5),Screen.width,(int)(buttonHeight/2)),"Do this");
//

		GUILayout.EndVertical();
		GUI.EndScrollView();

//		if(GUI.Button (new Rect(Screen.width * 0.55f, buttonHeight * 3.5f,shopWidth,buttonHeight), shop)) {
//			
//		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
