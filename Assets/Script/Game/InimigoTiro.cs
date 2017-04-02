using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoTiro : MonoBehaviour {
    public float m_velocidade;
    public AudioClip m_somDanoPlayer;
    int m_dano { get { return 1; } }
    Vector3 m_direcao;

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (OnScreen())
        {

            transform.Translate(m_direcao * m_velocidade);

        }
        else
        {

            Destroy(gameObject);

        }

	}


    private void OnCollisionEnter2D(Collision2D other)
    {


        if (other.gameObject.tag == "Player")
        {
            other.transform.GetComponent<PlayerControl>().levarDano(m_dano);
            AudioSource.PlayClipAtPoint(m_somDanoPlayer, GameObject.FindObjectOfType<Canvas>().transform.position);
            Destroy(gameObject);
        }
    }

    public void setDirection(GameObject m_posPlayer)
    {
        m_direcao = -(transform.position - m_posPlayer.transform.position).normalized;
    }

    bool OnScreen()
    {
        return  transform.position.x > -10 && transform.position.x < Screen.width + 10 &&
                transform.position.y > -10 && transform.position.y < Screen.height + 10;
    }
}
