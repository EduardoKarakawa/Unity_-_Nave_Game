using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificaScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GameObject.FindGameObjectWithTag("AllScores"))
        {
            GameObject.FindGameObjectWithTag("AllScores").GetComponent<HighScoreControl>().VerifarHighScore();
        }
	}

}
