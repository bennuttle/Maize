using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Marks : MonoBehaviour {
	void Start () {
		GameObject totalTimeSpent = GameObject.FindGameObjectWithTag ("TotalTimeSpent");
		totalTimeSpent.GetComponent<Text>().text = ((int)PlayerPrefs.GetFloat ("TotalTimeSpent")/60).ToString() + 
			":" + ((int)PlayerPrefs.GetFloat ("TotalTimeSpent")%60).ToString();

		GameObject totalSteps = GameObject.FindGameObjectWithTag ("TotalSteps");
		totalSteps.GetComponent<Text>().text = PlayerPrefs.GetInt ("TotalSteps").ToString();

		GameObject largestMaze = GameObject.FindGameObjectWithTag ("LargestMaze");
		largestMaze.GetComponent<Text>().text = PlayerPrefs.GetInt ("LargestMaze").ToString() + "x" + 
			PlayerPrefs.GetInt ("LargestMaze").ToString() + "x" + PlayerPrefs.GetInt ("LargestMaze").ToString();

		GameObject mazesCompleted = GameObject.FindGameObjectWithTag ("MazesCompleted");
		mazesCompleted.GetComponent<Text>().text = PlayerPrefs.GetInt ("MazesCompleted").ToString();

		int currentLevel = PlayerPrefs.GetInt ("Level");
		GameObject levelValue = GameObject.FindGameObjectWithTag ("LevelValue");
		levelValue.GetComponent<Text>().text = PlayerPrefs.GetInt ("Level").ToString();

		GameObject levelProgress = GameObject.FindGameObjectWithTag ("LevelProgress");
		levelProgress.GetComponent<Slider> ().maxValue = ((float)getExperienceReqForNextLevel (currentLevel));
		levelProgress.GetComponent<Slider>().value = (float)PlayerPrefs.GetInt ("Experience");
//		Debug.Log((float)PlayerPrefs.GetInt ("Experience"));
//		Debug.Log((float)getExperienceReqForNextLevel(currentLevel));
	}
	public static int getExperienceReqForNextLevel(int lev){
		int nextLevel = ++lev;
		int totalExpforNextLevel = 25 * nextLevel * nextLevel - 25 * nextLevel;
		return totalExpforNextLevel;
	}
}
