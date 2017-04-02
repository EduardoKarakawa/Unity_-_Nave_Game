using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogoPositivo : MonoBehaviour {

    // Use this for initialization
    void Start() {
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update() {

        if (transform.localScale.x < 1)
        {
            transform.localScale = transform.localScale + new Vector3(0.25f, 0.25f, 0.25f) * Time.deltaTime;
        }
        else
        {
            StartCoroutine(TrocarTela());
        }
		
	}

    IEnumerator TrocarTela()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Menu");
    }
}
