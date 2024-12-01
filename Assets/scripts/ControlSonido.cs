using UnityEngine;

public class ControlSonido : MonoBehaviour
{
    private bool isMuted = false; 

    //metodo para alternar el sonido
    public void ToggleSound()
    {
        isMuted = !isMuted; 
        AudioListener.pause = isMuted; 

    }
}
