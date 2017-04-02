using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {
    
    public GameObject[] m_objInimigo;
    public Text m_TextoVida;
    public Text m_TextoPontos;
    public Text m_TextoLevel;
    public AudioClip m_milestone;

    GameObject spanw;
    float m_tempoRespawn;
    int m_contPonto;

    bool m_criar;

    private void Start()
    {
        m_contPonto = 0;
        m_tempoRespawn = 2.0f;
        m_criar = false;
    }

    // Update is called once per frame
    void Update () {
        if (!m_criar)
        {
            m_criar = true;
            StartCoroutine(ContagemSpawn());
        }

        DiminuirTempoRespawInimigo();

        DiplayVida();
        DisplayPoints();
        DiasplayLevel();

        TrocarParaScores();

        if(SceneManager.GetActiveScene().name == "Scores")
        {
            Destroy(gameObject);
        }

    }




    // ------------------------ Funcoes ---------------------
    // Cria um inimigo aleatorio a cada 2 segundos
    void spawanarInimigo()
    {
        // Randomiza uma posicao fora da tela
        Vector3 spawnPosition = new Vector3(
                Screen.width + 100.0f,
                Random.Range(Screen.height - Screen.height * 0.95f, Screen.height * 0.95f),
                450
                );



        spanw = Instantiate(m_objInimigo[Random.Range(0, 3)], spawnPosition, transform.rotation);
    }


    // Instancia um inimigo e espera X segundos para criar outro
    IEnumerator ContagemSpawn()
    {
        spawanarInimigo();
        yield return new WaitForSeconds(m_tempoRespawn);
        m_criar = false;
    }


    void DiplayVida()
    {
        if(GameObject.Find("Player"))
            m_TextoVida.text = " x " + GameObject.Find("Player").GetComponent<PlayerControl>().GetVida().ToString();
    }

    void DisplayPoints()
    {
        if (GameObject.Find("Player"))
            m_TextoPontos.text = " Score: " + GameObject.Find("Player").GetComponent<PlayerControl>().GetPontos().ToString();
    }

    void DiasplayLevel()
    {
        if (GameObject.Find("Level"))
            m_TextoLevel.text = "Lvl: " + ((GameObject.Find("Player").GetComponent<PlayerControl>().GetPontos() + 500) / 500).ToString();
    }

    void TrocarParaScores()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().GetVida() <=0) {
            StartCoroutine(EsperarParaTrocar());
        }
    }

    IEnumerator EsperarParaTrocar()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Scores");
    }

    void DiminuirTempoRespawInimigo()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().GetPontos() - m_contPonto >= 500 &&
          m_tempoRespawn > 0.5f)
        {
            m_tempoRespawn -= 0.15f;
            m_contPonto = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().GetPontos();
            AudioSource.PlayClipAtPoint(m_milestone, GameObject.FindObjectOfType<Canvas>().transform.position);
        }
    }

}


