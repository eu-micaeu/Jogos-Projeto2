using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class scriptNpc : MonoBehaviour
{
    public Transform pc; 
    public float velocidade = 5f; 
    public float distanciaMinima = 1f; 

    void Start()
    {
    }

    void Update()
    {
        if (pc != null)
        {
            Vector3 direcao = pc.position - transform.position;
            float distancia = direcao.magnitude;

            if (distancia > distanciaMinima)
            {
                direcao.Normalize();
                transform.position += direcao * velocidade * Time.deltaTime;

                Quaternion rotacaoDesejada = Quaternion.LookRotation(direcao);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoDesejada, Time.deltaTime * velocidade);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pc"))
        {

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SceneManager.LoadScene("Game Over");
        }
    }


    public void Destruir()
    {
        Destroy(gameObject); 
    }
}
