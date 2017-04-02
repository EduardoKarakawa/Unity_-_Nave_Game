using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlesUI : MonoBehaviour {
    public Image m_Limite;
    public Image m_Analogico;

    Vector3 m_PosmLimite;
    Vector3 m_PosToque;
    Vector3 m_direction;


    float m_distancia;
    float m_raio;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        m_PosmLimite = m_Limite.rectTransform.position;

        m_PosToque = Input.GetTouch(0).position;
        // TODO m_PosToque = Input.mousePosition;
        ControleAnalogico();

    }

    void ControleAnalogico() {
        if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary))
        {
            m_raio = m_Limite.rectTransform.rect.width / 2.0f;


            m_direction = (m_PosToque - m_PosmLimite).normalized;

            m_distancia = Vector2.Distance(m_PosmLimite, m_PosToque);

            m_distancia = m_distancia > m_raio ? m_raio : m_distancia;

            Vector3 tmp = m_PosmLimite - (m_PosmLimite - m_PosToque).normalized * m_distancia;

            m_Analogico.rectTransform.position = tmp;

        }
        else
        {
            m_Analogico.rectTransform.position = m_PosmLimite;
            m_direction = new Vector3(0, 0, 0);
        }
    }

    public Vector3 GetDirection() { return m_direction; }
}

