using UnityEngine;

public class Salir : MonoBehaviour
{
    public void ExitApplication()
    {
        #if UNITY_EDITOR
            // si estamos en el editor de Unity solo se detiene la ejecución
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            //si estamos en el exe cerramos la aplicacion
            Application.Quit();
        #endif
    }
}
