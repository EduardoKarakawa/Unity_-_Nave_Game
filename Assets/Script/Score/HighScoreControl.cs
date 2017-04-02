using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreControl : MonoBehaviour {
    int m_pontoAtual;
    Text m_TextoHighScore;

    GameObject aux;

    Score[] m_listHighScore;

    string m_nome;
    bool m_inseriu;
    int m_cont;


	// Use this for initialization
	void Start () {
        m_nome = "";
    }
	
	// Update is called once per frame
	void Update () {

        
        if(GameObject.FindGameObjectWithTag("ListHighScore"))
        {
            DisplayHighScore();
        }

        Inserir();



    }

    public void VerifarHighScore()
    {


        if (SceneManager.GetActiveScene().name == "Scores")
        {
            m_TextoHighScore = GameObject.FindGameObjectWithTag("ListHighScore").GetComponent<Text>();


            for (int i = 0; i < m_listHighScore.Length; i++)
            {
                if (m_pontoAtual > m_listHighScore[i].GetPonto())
                {
                    pegarNome();
                    m_cont = i;
                    m_inseriu = false;
                    break;
                }
            }

        }Debug.Log("Teminado");
    }

    void DisplayHighScore()
    {
        string tmp = "";
        for(int i = 0; i < m_listHighScore.Length; i++)
        {
            tmp += m_listHighScore[i].GetNome() + "  -  " + m_listHighScore[i].GetPonto().ToString() + "\n";
        }
        m_TextoHighScore.text = tmp;
    }

    // Pega os pontos ganhos
    public void SetPontos(int ponto)
    {
        m_pontoAtual = ponto;
    }

    void pegarNome()
    {
        aux = Instantiate(Resources.Load("Score/DigitarNome") as GameObject);
        aux.transform.parent = GameObject.Find("Canvas").transform;
        aux.GetComponent<InputField>().onEndEdit.AddListener(novoNome);
    }

    void novoNome(string arg0)
    {
        m_nome = arg0;
    }

    void Inserir()
    {
        if (m_nome.Length > 3 && !m_inseriu)
        {
            OrdenaLista();
            m_listHighScore[m_cont].SetNome(m_nome);
            m_listHighScore[m_cont].SetPonto(m_pontoAtual);

            m_inseriu = true;
        }
        if (m_inseriu)
        {
            ResetTudo();
        }
            
    }

    void ResetTudo()
    {
        Destroy(GameObject.FindGameObjectWithTag("DigitarNome"));
        m_inseriu = false;
        m_cont = 0;
        m_pontoAtual = 0;
        m_nome = "";
        aux = null;
    }
    public void StarList()
    {
        if(m_listHighScore == null)
        {
            m_listHighScore = new Score[5];
            for (int i = 0; i < m_listHighScore.Length; i++)
            {
                m_listHighScore[i] = new Score("--", 0);
            }

        }
        
    }

    void OrdenaLista()
    {
        if(m_listHighScore[m_cont].GetPonto() > 0)
        {
            Debug.Log("Ordenando            --------------------------------------");
            for (int i = m_listHighScore.Length - 1; i > m_cont ; i--)
            {
                    Debug.Log(m_listHighScore[i].GetPonto().ToString() + "  " + i.ToString() + "   ");
                    m_listHighScore[i] = m_listHighScore[i - 1];
                
            }
            m_listHighScore[m_cont] = new Score();
        }

    }
}
