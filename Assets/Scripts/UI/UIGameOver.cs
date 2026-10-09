using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] ScoreKeeper scoreKeeper;


    void Awake()
    {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }

    void Start()
    {
        scoreText.text = "Final Score: \n" + scoreKeeper.GetScore().ToString();
    }
}
