using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarEscenaPrincipal : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        // Verificar si existe la instancia del GameManager
        if (GameManager.Instance != null)
        {
            // Reiniciar el puntaje
            GameManager.Instance.ResetScore();
        }

        // Cargar la escena principal
        SceneManager.LoadScene(sceneName);
    }
}
