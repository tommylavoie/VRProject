﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame(int handicap)
    {
        PlayerPrefs.SetInt("Handicap", handicap);
        SceneManager.LoadScene("MainScene");
    }
}
