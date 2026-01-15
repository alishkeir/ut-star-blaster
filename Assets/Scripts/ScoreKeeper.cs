using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    int score = 0;


    public int GetScore()
    {
        return score;
    }

    public void ModifyScore(int amount)
    {
        score += amount;
        score = Mathf.Clamp(score, 0, int.MaxValue);
        print(score);
    }

    public void ResetScore()
    {
        score = 0;
    }
}
