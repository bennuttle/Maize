﻿using UnityEngine;
using System.Collections;

public class PreferencesCreation : MonoBehaviour {



	//Goal is to make sure PlayerPrefs is set up.
	//If it is not, instantiate it.
	void Start () {
		if (!PlayerPrefs.HasKey ("TotalSteps"))	PlayerPrefs.SetInt ("TotalSteps", 0);
		if (!PlayerPrefs.HasKey ("Level"))	PlayerPrefs.SetInt ("Level", 0);
		if (!PlayerPrefs.HasKey ("Experience"))	PlayerPrefs.SetInt ("Experience", 0);
		if (!PlayerPrefs.HasKey ("TotalTurns"))	PlayerPrefs.SetFloat ("TotalTimeSpent", 0f);
		if (!PlayerPrefs.HasKey("TotalTimeSpent")) PlayerPrefs.SetInt ("TotalTurns", 0);
		if (!PlayerPrefs.HasKey("LargestMaze")) PlayerPrefs.SetInt ("LargestMaze", 0);
		PlayerPrefs.Save ();
	}

}