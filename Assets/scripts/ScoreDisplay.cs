using UnityEngine;
using TMPro; // Para usar TextMeshPro

public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    void Start()
    {
        // Obtén el componente de texto
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Actualiza el texto con el puntaje actual
        scoreText.text = "Puntos: " + GameManager.Instance.GetScore();
    }
}
