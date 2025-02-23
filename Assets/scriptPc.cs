using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public AudioSource som1; 
    public AudioSource som2;
    public AudioSource som3;
    public TextMeshProUGUI textoPontos; 
    public TextMeshProUGUI textoMunicao; 

    private int pontos = 0;
    private int municaoAtual = 10; 
    private int tamanhoCarregador = 10; 

    void Start()
    {
        rbd = GetComponent<Rigidbody>();
        rotIni = transform.localRotation;
        dist = 100;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AtualizarTextoMunicao();
    }

    void Update()
    {
        Movimentacao();
        RotacaoCamera();
        Interacao();
        LimiteMapa();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Recarregar();
        }
        
        if (pontos >= 40)
        {
	    SceneManager.LoadScene("Game Over");
        }
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

        transform.Rotate(Vector3.up * mouseX);

        rotacaoY -= mouseY;
        rotacaoY = Mathf.Clamp(rotacaoY, -90f, 90f);

        cabeca.transform.localRotation = Quaternion.Euler(rotacaoY, 0f, 0f);
    }

    void Interacao()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (municaoAtual > 0)
            {
                Atirar();
            }
            else
            {
                Debug.Log("Sem munição!");
            }
        }
    }

    void Atirar()
    {
        som1.Play();
        municaoAtual--; 
        AtualizarTextoMunicao(); 

        if (Physics.Raycast(cabeca.transform.position, cabeca.transform.forward, out RaycastHit hit, dist, mascara))
        {
            if (hit.collider.CompareTag("Npc"))
            {
                scriptNpc npc = hit.collider.GetComponent<scriptNpc>();
                if (npc != null)
                {
                    npc.Destruir();
                }
            }
            else
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

    public void AdicionarPontos(int quantidade)
    {
        pontos += quantidade;
        textoPontos.text = "Pontos: " + pontos.ToString(); 
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ponto"))
        {
            som2.Play();
            Destroy(collision.gameObject);
            AdicionarPontos(1); 
        }
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            som3.Play();
            Destroy(collision.gameObject);
            Recarregar(); 
        }
    }

    void Recarregar()
    {
        municaoAtual = tamanhoCarregador;
        AtualizarTextoMunicao();
    }

    void AtualizarTextoMunicao()
    {
        textoMunicao.text = "Munição: " + municaoAtual.ToString() + "/" + tamanhoCarregador.ToString();
    }
}
