using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text highScoreText;

    public void CheckHighScore()
    {
        if (GameManager.score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", GameManager.score);
        }

        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");

        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore").ToString();
    }
}
