using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int score = 0; // Puntos
    private AudioSource audioSource;

    public string escenaAudioActiva = "JuegoPrincipal"; 

    private void Awake()
    {
        //configurar el singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Verificar el nombre de la escena actual
        if (SceneManager.GetActiveScene().name == escenaAudioActiva)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Puntos: " + score);
    }

    public int GetScore()
    {
        return score;
    }
    public void ResetScore()
    {
        score = 0;
        Debug.Log("Puntaje reiniciado: " + score);
    }
}
