using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject PanelPausa;

    private bool isPaused = false; // Indica si el juego está pausado o no

    void Update()
    {
        // Detecta si se presiona la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(); // Alterna entre pausar y reanudar
        }
    }

    // Método para alternar el estado de pausa
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

    // Pausa el juego
    private void PauseGame()
    {
        Time.timeScale = 0f; // Detiene el tiempo del juego
        isPaused = true;
        // Aquí puedes activar un menú de pausa, si tienes uno
        Debug.Log("Juego pausado");
        PanelPausa.SetActive(true);
    }

    // Reanuda el juego
    private void ResumeGame()
    {
        Time.timeScale = 1f; // Restaura el tiempo del juego
        isPaused = false;
        // Aquí puedes desactivar un menú de pausa, si tienes uno
        Debug.Log("Juego reanudado");
        PanelPausa.SetActive(false);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f; // Restablece el tiempo por si está pausado
        SceneManager.LoadScene("MenuInicio"); // Cambia a la escena del menú de inicio
    }
}

