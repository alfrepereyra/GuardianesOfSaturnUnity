using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainButton : MonoBehaviour
{
    public void ReloadCurrentScene()
    {

        GameManager.Instance.ResetScore();

        //recarga la escena para reiniciar todo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
