using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class scriptGameOver : MonoBehaviour
{
    public GameObject btnSair;
    public GameObject btnReiniciar;

    void Start()
    {
    	btnSair.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnSairClick);
        btnReiniciar.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnIniciarClick);
    }

    void OnSairClick()
    {
        SceneManager.LoadScene("Tela inicial");
    }

    void OnIniciarClick()
    {
        SceneManager.LoadScene("Jogo");
    }
}
