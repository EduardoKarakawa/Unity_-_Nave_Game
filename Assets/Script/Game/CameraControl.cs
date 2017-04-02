using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	void Start () {
        
    }
    void Update()
    {
        transform.position = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0);
    }
}
