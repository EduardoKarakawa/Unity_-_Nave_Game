using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (!GameObject.FindGameObjectWithTag("AllScores"))
        {
            GameObject aux = Instantiate(Resources.Load("Score/AllScores") as GameObject);
            aux.GetComponent<HighScoreControl>().StarList();
        }
	}

}
