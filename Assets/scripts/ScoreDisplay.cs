using UnityEngine;
using TMPro; 

public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }
// actualiza el texto con el puntaje actual
    void Update()
    {
        scoreText.text = "SCORE: " + GameManager.Instance.GetScore();
    }
}
