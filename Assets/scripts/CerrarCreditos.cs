using UnityEngine;
using UnityEngine.SceneManagement;

public class CerrarCreditos : MonoBehaviour
{
 public void OnClickCerrar()
 {
    SceneManager.LoadScene("MenuInicio");
 }
}
