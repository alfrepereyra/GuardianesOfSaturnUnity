using UnityEngine;

public class ControlSonido : MonoBehaviour
{
    private bool isMuted = false; // Estado del sonido

    // Método para alternar el sonido
    public void ToggleSound()
    {
        isMuted = !isMuted; // Alterna entre activado y desactivado
        AudioListener.pause = isMuted; // Pausa o reanuda todos los sonidos

        // Opcional: Cambiar visualización del botón (por ejemplo, icono)
        Debug.Log(isMuted ? "Sonido desactivado" : "Sonido activado");
    }
}
