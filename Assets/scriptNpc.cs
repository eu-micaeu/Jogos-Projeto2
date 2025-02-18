using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Adicione esta linha para usar o SceneManager

public class scriptNpc : MonoBehaviour
{
    public Transform pc; // Refer�ncia ao objeto Pc
    public float velocidade = 5f; // Velocidade de persegui��o
    public float distanciaMinima = 1f; // Dist�ncia m�nima para parar de perseguir

    // Start � chamado antes da primeira atualiza��o do frame
    void Start()
    {
        // Inicializa��o, se necess�rio
    }

    // Update � chamado uma vez por frame
    void Update()
    {
        if (pc != null)
        {
            // Calcula a dire��o para o objeto Pc
            Vector3 direcao = pc.position - transform.position;
            float distancia = direcao.magnitude;

            // Se a dist�ncia for maior que a dist�ncia m�nima, move o NPC
            if (distancia > distanciaMinima)
            {
                direcao.Normalize();
                transform.position += direcao * velocidade * Time.deltaTime;

                // Rotaciona o NPC para enfrentar o PC
                Quaternion rotacaoDesejada = Quaternion.LookRotation(direcao);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoDesejada, Time.deltaTime * velocidade);
            }
        }
    }

    // M�todo chamado ao colidir com outro objeto
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pc"))
        {

            // Carrega a cena "Game Over"
            SceneManager.LoadScene("Game Over");
        }
    }
}