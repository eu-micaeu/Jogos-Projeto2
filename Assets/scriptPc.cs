using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scriptPc : MonoBehaviour
{
    private Rigidbody rbd;
    public float vel = 10;
    private Quaternion rotIni;
    public float sensibilidadeMouse = 100f;
    private float rotacaoY = 0;
    public GameObject cabeca;
    public LayerMask mascara;
    public float dist;
    private AudioSource som;
    public TextMeshProUGUI textoPontos; 

    private int pontos = 0;

    void Start()
    {
        rbd = GetComponent<Rigidbody>();
        som = GetComponent<AudioSource>();
        rotIni = transform.localRotation;
        dist = 100;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Movimentacao();
        RotacaoCamera();
        Interacao();
        LimiteMapa();
    }

    void Movimentacao()
    {
        float frente = Input.GetAxis("Vertical");
        float lado = Input.GetAxis("Horizontal");

        rbd.velocity = transform.TransformDirection(new Vector3(lado * vel, rbd.velocity.y, frente * vel));
    }

    void RotacaoCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadeMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadeMouse * Time.deltaTime;

        // Rotação horizontal (giro do personagem)
        transform.Rotate(Vector3.up * mouseX);

        // Rotação vertical (câmera)
        rotacaoY -= mouseY;
        rotacaoY = Mathf.Clamp(rotacaoY, -90f, 90f);

        cabeca.transform.localRotation = Quaternion.Euler(rotacaoY, 0f, 0f);
    }

    void Interacao()
    {
        if (Input.GetMouseButtonDown(0))
        {
            som.Play();

            if (Physics.Raycast(cabeca.transform.position, cabeca.transform.forward, out RaycastHit hit, dist, mascara))
            {
                Rigidbody rbd = hit.collider.GetComponent<Rigidbody>();
                if (rbd != null)
                {
                    rbd.AddForce(cabeca.transform.forward * 500);
                }
            }
        }
    }

    void LimiteMapa()
    {
        if (transform.position.z > 50)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -45);
        }
        else if (transform.position.z < -48)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 45);
        }
    }

    // Método para adicionar pontos
    public void AdicionarPontos(int quantidade)
    {
        pontos += quantidade;
        textoPontos.text = "Pontos: " + pontos.ToString(); // Atualiza o texto da UI sempre que os pontos mudam
    }

    // Método para detectar colisão com objetos com a tag "Ponto"
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ponto"))
        {
            Destroy(collision.gameObject);
            AdicionarPontos(1); // Adiciona 1 ponto ao destruir o objeto
        }
    }
}