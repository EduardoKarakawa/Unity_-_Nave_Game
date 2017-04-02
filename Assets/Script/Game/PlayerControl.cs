using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerControl : MonoBehaviour {
    public ControlesUI m_controle;
    public GameObject m_tiro;
    public AudioClip m_somMorreu;
    public int m_vida;
    public Text teste;

    int m_proxVida;
    Rigidbody2D m_playerRigidbody;
    Vector2 m_velocity;



    bool m_atirou;
    int m_pontos;

    int MAX_Velocity { get{ return 7;} }

    // Use this for initialization
    void Start () {
        m_vida = 3;
        m_pontos = 0;
        m_playerRigidbody = GetComponent<Rigidbody2D>();
        m_velocity = new Vector2(0, 0);
        m_atirou = false;
        m_proxVida = 0;
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 aux = new Vector3(m_controle.GetDirection().x * m_velocity.x, m_controle.GetDirection().y * m_velocity.y, 0);
        Vector2 tmp = Camera.main.ScreenToWorldPoint(m_playerRigidbody.transform.position + aux);

        // Se o player ainda tiver vivo, ele atualizado
        if (Vivo())
        {
            AumetarVelocidade(tmp);
            AtingiuBorda(tmp);
            Atirar();
            GanharVida();
            m_playerRigidbody.transform.Translate(aux);
        }
        // Se nao ele é destruido
        else
        {
            AudioSource.PlayClipAtPoint(m_somMorreu, GameObject.FindObjectOfType<Canvas>().transform.position);
            GameObject.FindGameObjectWithTag("AllScores").GetComponent<HighScoreControl>().SetPontos(m_pontos);
        }



    }



    //-------------- Funcoes --------------------

    // Aumenta a velocidade para x e y separadamente
    void AumetarVelocidade(Vector3 proxPos)
    {
        if (proxPos.x >= 0 && proxPos.x <= Screen.width && proxPos.y >= 0 && proxPos.y <= Screen.height)
        {
            if (m_velocity.x < MAX_Velocity)
                m_velocity.x += MAX_Velocity * Time.deltaTime;

            if (m_velocity.y < MAX_Velocity)
                m_velocity.y += MAX_Velocity * Time.deltaTime;
        }
    }


    // Verifica se atingiu a borda da tela e inverte a velocidade
    void AtingiuBorda(Vector3 proxPos)
    {

 
        teste.text = "asd" + proxPos + "  " ;
        if (proxPos.x < 0 || proxPos.x > Screen.width)
            m_velocity.x = -m_velocity.x;

        if (proxPos.y < 0 || proxPos.y > Screen.height)
            m_velocity.y = -m_velocity.y;
    }


    // Aplica dano no jogador
    public void levarDano(int dano)
    {
        m_vida -= dano;
    }

    // Soma os pontos ganhos em m_pontos
    public void SomarPonto(int pontos)
    {
        m_pontos += pontos;
    }

    public int GetVida() { return m_vida; }

    public int GetPontos() { return m_pontos; }


    // Verifica se a tecla foi precionada e se ainda n atirou
    void Atirar()
    {

        if (Input.touchCount > 1 && (Input.GetTouch(1).phase == TouchPhase.Began) && !m_atirou) // || Input.GetKeyDown(KeyCode.Space) ))
        {
            m_atirou = true;
            StartCoroutine( ColdownAtirar());
        }
    }

    
    // Realiza o spawn do tiro e aguarda X segundos para poder atirar de novo
    IEnumerator ColdownAtirar()
    {
        Instantiate(m_tiro, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.75f);
        m_atirou = false;

    }

    // Verifica se colidiu com um Inimigo
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Inimigo")
        {
            levarDano(1);
        }
    }

    // Retorna se o player esta vivo
    bool Vivo()
    {
        if(m_vida > 0)
        {
            return true;
        }
        return false;
    }

  
    void GanharVida()
    {
        if(m_pontos - m_proxVida >= 1000)
        {
            m_vida++;
            m_proxVida = m_pontos;
        }
    }


}
