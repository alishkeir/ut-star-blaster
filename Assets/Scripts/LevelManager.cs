using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }



    public void LoadGame()
    {
        scoreKeeper.ResetScore();

        SceneManager.LoadScene("GameScene");
        Cursor.visible = false;
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver", 1f));
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Cursor.visible = true;
    }


    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (sceneName != "GameScene")
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }


        SceneManager.LoadScene(sceneName);
    }

}
