using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton para acceso global

    private int score = 0; // Puntos del jugador

    private void Awake()
    {
        // Configurar el Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre escenas
        }
        else
        {
            Destroy(gameObject);
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
}
