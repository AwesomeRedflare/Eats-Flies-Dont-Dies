using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text finalScoreText;
    public Text winScoreText;
    public static int score;

    public static int health;
    public int numberOfHearts;

    public SpriteRenderer[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHearth;

    public GameObject gameOverPanel;
    public GameObject winPanel;
    public GameObject transition;

    public GameObject playerHurtEffect;
    private int whichHeart;

    private void Start()
    {
        transition.SetActive(true);
        score = 0;
        whichHeart = numberOfHearts - 1;
        health = numberOfHearts;
    }

    private void Update()
    {
        scoreText.text = "Score: " + score;

        if (health > numberOfHearts)
        {
            health = numberOfHearts;
        }


        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHearth;

                if(i == whichHeart)
                {
                    Instantiate(playerHurtEffect, hearts[i].transform.position, Quaternion.identity);

                    whichHeart--;
                }
            }

            /*
            if (i < numberOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
            */
        }
    }

    void CloseTransition()
    {
        transition.GetComponent<Animator>().SetTrigger("Close");
    }

    public void RetryButton()
    {
        FindObjectOfType<AudioManager>().Play("Button");

        CloseTransition();

        Invoke("ResetScene", .6f);
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButton()
    {
        FindObjectOfType<AudioManager>().Play("Button");

        CloseTransition();

        Invoke("GoToMainMenu", .6f);
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CheckHealth()
    {
        if (health <= 0)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");


            if (FindObjectOfType<HighScore>() != null)
            {
                FindObjectOfType<HighScore>().CheckHighScore();
            }
            FindObjectOfType<AudioManager>().Play("Death");
            finalScoreText.text = "Final Score: " + score;
            Invoke("OpenGameOverPanel", .6f);
            Instantiate(playerHurtEffect, player.transform.position, Quaternion.identity);
            player.SetActive(false);
        }
    }

    public void Win()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        FindObjectOfType<AudioManager>().Play("Pickup");
        winScoreText.text = "Final Score: " + score;
        winPanel.SetActive(true);
        Instantiate(playerHurtEffect, player.transform.position, Quaternion.identity);
        player.SetActive(false);
    }

    void OpenGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}
