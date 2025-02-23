using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class scriptTelaInicial : MonoBehaviour
{
    public GameObject btnSair;
    public GameObject btnIniciar;

    void Start()
    {
        btnSair.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnSairClick);
        btnIniciar.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnIniciarClick);
    }

    void Update()
    {

    }

    void OnSairClick()
    {
        Application.Quit();
    }

    void OnIniciarClick()
    {
        SceneManager.LoadScene("Jogo");
    }
}
