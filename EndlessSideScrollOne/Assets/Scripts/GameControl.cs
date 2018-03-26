using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{

    public GameObject gameOverpanel;
    public Text sText;
    int score = 0;
    public Text highScore;
    public Text currentScore;
    public GameObject newScoreAlert;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GameOver()
    {
        Invoke("ShowOverPanel", 2.0f);
    }
    void ShowOverPanel()
    {
        sText.gameObject.SetActive(false);

        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);

            newScoreAlert.SetActive(true);
        }
        highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        currentScore.text = "Current Score: " + score.ToString();

        gameOverpanel.SetActive(true);
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }
    public void ScoreCalc() //Function to increment player score +1 each time player touches a 'coin'
    {
        score++;
        sText.text = score.ToString();
    }

    public void QuitGame() //added to be able to use the Exit Game button if player loses.
    {
        Application.Quit();
    }
}
