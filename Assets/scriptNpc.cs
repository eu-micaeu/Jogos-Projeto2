using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Adicione esta linha para usar o SceneManager

public class scriptNpc : MonoBehaviour
{
    public Transform pc; // Referência ao objeto Pc
    public float velocidade = 5f; // Velocidade de perseguição
    public float distanciaMinima = 1f; // Distância mínima para parar de perseguir

    // Start é chamado antes da primeira atualização do frame
    void Start()
    {
        // Inicialização, se necessário
    }

    // Update é chamado uma vez por frame
    void Update()
    {
        if (pc != null)
        {
            // Calcula a direção para o objeto Pc
            Vector3 direcao = pc.position - transform.position;
            float distancia = direcao.magnitude;

            // Se a distância for maior que a distância mínima, move o NPC
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

    // Método chamado ao colidir com outro objeto
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pc"))
        {

            // Carrega a cena "Game Over"
            SceneManager.LoadScene("Game Over");
        }
    }
}