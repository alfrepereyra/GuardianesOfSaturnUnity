using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject PanelPausa;

    private bool isPaused = false; 

    void Update()
    {
        //detecta la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(); 
        }
    }

    //alternar el estado de pausa
    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

   
    private void PauseGame()
    {
        Time.timeScale = 0f; // Detiene el tiempo del juego
        isPaused = true;
        Debug.Log("Juego pausado");
        PanelPausa.SetActive(true);
    }

    //despausar
    private void ResumeGame()
    {
        Time.timeScale = 1f; //restaura el tiempo del juego
        isPaused = false;
        PanelPausa.SetActive(false);
    }


    //salir al menu
    public void QuitToMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MenuInicio");
    }
}

