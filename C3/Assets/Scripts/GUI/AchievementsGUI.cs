using UnityEngine;
using System.Collections;

//This class will be used to create the Marks Scene
public class AchievementsGUI : MonoBehaviour {

	public GUISkin achievementsSkin;
	public Texture2D logo;
	public Texture2D achievementsIcon;
	public Texture2D red, blue, yellow, green, grey, purple;
	
	private float totalTime;
	private int totalSteps;
	private int totalTurns;
	private int maxLevel;
	private string max;
	private string timeSpent;
	private int exp;
	private int currentLevel;
	private float progressWidth;
	private float progressRatio;



	//Sets all the playerpref values to variables for easy reference
	void Start () {
		totalTime = PlayerPrefs.GetFloat ("TotalTimeSpent");
		totalSteps = PlayerPrefs.GetInt ("TotalSteps");
		totalTurns = PlayerPrefs.GetInt ("TotalTurns");
		maxLevel = PlayerPrefs.GetInt ("LargestMaze");
		exp = PlayerPrefs.GetInt ("Experience");
		currentLevel = PlayerPrefs.GetInt ("Level");
		progressWidth = Screen.width * 0.84f;
		progressRatio = (float)exp / ((float)getExperienceReqForNextLevel(currentLevel));


		max = maxLevel + "x" + maxLevel + "x" + maxLevel;

		string minutes = ((int) totalTime / 60).ToString ();
		string seconds = ((int) totalTime % 60).ToString ();

		if (seconds.Length == 1)
						seconds = "0" + seconds;

		timeSpent = minutes + ":" + seconds;
	}
	public static int getExperienceReqForNextLevel(int lev){
		int nextLevel = ++lev;
		int totalExpforNextLevel = 25 * nextLevel * nextLevel - 25 * nextLevel;
		return totalExpforNextLevel;
	}

	void OnGUI () {
		GUI.skin = achievementsSkin;

		//Background box to make a border
		//GUI.Box (new Rect (0.0f, 0.0f, Screen.width, Screen.height * 0.82f), new GUIContent(""), GUI.skin.GetStyle ("blackBorder"));

		//Marks display at the top (needs to be made yellow)
		
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height * 0.2f), achievementsIcon);

		//(would prefer quad boxes to be another color, potentially to much yellow when we change title.)

		//Total Steps Achievements
//		GUI.Box (new Rect (0.0f, Screen.height * 0.2f, Screen.width * 0.495f, Screen.height * 0.1f), new GUIContent ("Complete 10 Mazes"), GUI.skin.GetStyle ("totalStepsHeader"));
//		GUI.Box (new Rect (0.0f, Screen.height * 0.2f, Screen.width * 0.495f, Screen.height * 0.1f), new GUIContent (totalSteps.ToString ()), GUI.skin.GetStyle ("totalStepsBody"));
		//topleft
		GUI.DrawTexture (new Rect (0.0f, Screen.height * 0.2f, Screen.width, Screen.height * 0.1f), yellow);
		GUI.DrawTexture (new Rect (0.0f, (Screen.height * 0.3f)+5, Screen.width, Screen.height * 0.1f), yellow);
		GUI.DrawTexture (new Rect (0.0f, (Screen.height * 0.4f) + 10, Screen.width, Screen.height * 0.1f), yellow);
		GUI.DrawTexture (new Rect (0.0f, (Screen.height * 0.5f)+15, Screen.width, Screen.height * 0.1f), yellow);
		GUI.DrawTexture (new Rect (0.0f, (Screen.height * 0.6f)+20, Screen.width, Screen.height * 0.1f), yellow);
		GUI.DrawTexture (new Rect (0.0f, (Screen.height * 0.7f) + 25, Screen.width, Screen.height * 0.1f), yellow);
////
//		GUI.DrawTexture (new Rect (Screen.width * 0.505f, Screen.height * 0.2f, Screen.width * 0.495f, Screen.height * 0.1f), yellow);
//
//		GUI.DrawTexture (new Rect (0.0f, Screen.height * 0.51f, Screen.width * 0.495f, Screen.height * 0.1f), yellow);
//
//		GUI.DrawTexture (new Rect (Screen.width * 0.505f, Screen.height * 0.51f, Screen.width * 0.495f, Screen.height * 0.1f), yellow);

//		//Total Turns Achivements
//		GUI.Box (new Rect (Screen.width * 0.505f, Screen.height * 0.2f, Screen.width * 0.495f, Screen.height * 0.1f), new GUIContent ("Reach Level 20"), GUI.skin.GetStyle ("totalTurnsHeader"));
//		GUI.Box (new Rect (Screen.width * 0.505f, Screen.height * 0.3f, Screen.width * 0.495f, Screen.height * 0.2f), new GUIContent (totalTurns.ToString ()), GUI.skin.GetStyle ("totalTurnsBody"));
//
//		//Time Played
//		GUI.Box (new Rect (0.0f, Screen.height * 0.51f, Screen.width * 0.495f, Screen.height * 0.1f), new GUIContent ("Take 500 Steps"), GUI.skin.GetStyle ("totalTurnsHeader"));
//		GUI.Box (new Rect (0.0f, Screen.height * 0.61f, Screen.width * 0.495f, Screen.height * 0.2f), new GUIContent (timeSpent), GUI.skin.GetStyle ("totalTurnsBody"));
//
//		//Largest Maze Completed
//		GUI.Box (new Rect (Screen.width * 0.505f, Screen.height * 0.51f, Screen.width * 0.495f, Screen.height * 0.1f), new GUIContent ("Complete a 5x5x5"), GUI.skin.GetStyle ("totalTurnsHeader"));
//		GUI.Box (new Rect (Screen.width * 0.505f, Screen.height * 0.61f, Screen.width * 0.495f, Screen.height * 0.2f), new GUIContent (max), GUI.skin.GetStyle ("totalTurnsBody"));

		//Draw box around level's label. (Needs cleaning up)
		//GUI.DrawTexture (new Rect (0.0f, Screen.height * 0.8f, Screen.width, Screen.height * 0.1f), marksRecs);

	
		//Back button in the bottom right
		if (GUI.Button (new Rect (Screen.width * 0.6f, Screen.height * 0.9f, Screen.width * 0.4f, Screen.height * 0.1f), new GUIContent ("Back"), GUI.skin.GetStyle ("backButton"))) {
			Application.LoadLevel (3);
		}
	}
}
