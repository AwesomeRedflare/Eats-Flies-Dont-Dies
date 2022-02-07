using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject transition;

    private void Start()
    {
        transition.SetActive(true);
    }

    void CloseTransition()
    {
        transition.GetComponent<Animator>().SetTrigger("Close");
    }

    public void EndlessModeButton()
    {
        FindObjectOfType<AudioManager>().Play("Button");

        CloseTransition();

        Invoke("Play", .6f);
    }

    void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void LevelOneButton()
    {
        FindObjectOfType<AudioManager>().Play("Button");

        CloseTransition();

        Invoke("LevelOne", .6f);
    }

    void LevelOne()
    {
        SceneManager.LoadScene("LevelOne");
    }
}
