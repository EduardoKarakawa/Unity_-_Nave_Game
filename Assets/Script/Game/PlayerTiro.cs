using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTiro : MonoBehaviour {
    public float m_velocidade;
    int m_dano { get { return 1; } }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (OnScreen())
        {
            transform.Translate(Vector3.right * m_velocidade);
        }

        else
        {
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.tag == "Inimigo")
        {
            other.transform.GetComponent<InimigoControl>().LevarDano(m_dano);
            Destroy(gameObject);
        }
    }
    bool OnScreen()
    {
        return transform.position.x > -10 && transform.position.x < Screen.width + 10 &&
                transform.position.y > -10 && transform.position.y < Screen.height + 10;
    }
}
