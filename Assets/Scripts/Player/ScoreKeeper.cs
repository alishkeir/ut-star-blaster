using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    int score = 0;

    static ScoreKeeper instance = null;


    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }


    public int GetScore()
    {
        return score;
    }

    public void ModifyScore(int amount)
    {
        score += amount;
        score = Mathf.Clamp(score, 0, int.MaxValue);
    }

    public void ResetScore()
    {
        score = 0;
    }
}
