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
        if (btnSair == null)
        {
            Debug.LogError("btnSair is not assigned in the Inspector!");
        }
        else
        {
            btnSair.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnSairClick);
        }

        if (btnReiniciar == null)
        {
            Debug.LogError("btnReiniciar is not assigned in the Inspector!");
        }
        else
        {
            btnReiniciar.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnIniciarClick);
        }
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
