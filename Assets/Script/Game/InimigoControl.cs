using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoControl : MonoBehaviour {
    public AudioClip m_explosao;

    public int m_vida, m_pontos;
    public float m_velocidade;
    public bool m_atira;
    public GameObject m_tiro;


    bool m_atirou;
    int m_dano { get { return 1; } }
    float coldownFire { get { return 1.5f;} }   
    
	// Use this for initialization
	void Start () {
        m_atirou = false;
    }
	
	// Update is called once per frame
	void Update () {

        transform.Translate(-transform.right * m_velocidade);


        Destruir();

        if (!Vivo())
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().SomarPonto(m_pontos);
        }

        if (m_atira && !m_atirou && GameObject.FindGameObjectWithTag("Player"))
        {
            m_atirou = true;
            StartCoroutine( Fire());
            
        }

	}


    IEnumerator Fire()
    {
        GameObject aux = Instantiate(m_tiro, transform.position, transform.rotation);
        aux.transform.GetComponent<InimigoTiro>().setDirection(GameObject.FindGameObjectWithTag("Player"));
        yield return new WaitForSeconds(coldownFire);
        m_atirou = false;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }


    public void LevarDano(int dano)
    {
        m_vida -= dano;
    }


    bool Vivo()
    {

        return m_vida > 0;
        
    }

    void Destruir()
    {
        if (transform.position.x <= -10 || !Vivo())
        {
            AudioSource.PlayClipAtPoint(m_explosao, GameObject.FindObjectOfType<Camera>().transform.position);
            Destroy(gameObject);
        }
    }


}
