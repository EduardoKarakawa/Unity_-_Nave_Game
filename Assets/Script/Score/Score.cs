using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    public int m_ponto;
    public string m_nome;

    public Score()
    {
        m_ponto = 0;
        m_nome = "--";
    }

    public Score(string nome, int ponto)
    {
        m_ponto = ponto;
        m_nome = nome;
    }

    public void SetPonto(int ponto) { m_ponto = ponto; }

    public void SetNome(string nome) { m_nome = nome; }

    public int GetPonto() { return m_ponto; }

    public string GetNome() { return m_nome; }

}
