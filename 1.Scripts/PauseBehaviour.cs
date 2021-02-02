using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseBehaviour : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject happy;
    public GameObject sad;
    public GameObject fear;
    public GameObject angry;
    public GameObject pause;
    public GameObject spawn;
    public GameObject rotate;

    public AudioSource UI;

    public void Pause()
    {
        pausePanel.SetActive(true);
        happy.SetActive(false);
        sad.SetActive(false);
        fear.SetActive(false);
        angry.SetActive(false);
        pause.SetActive(false);
        spawn.SetActive(false);
        rotate.SetActive(false);
        Time.timeScale = 0;
        UI.Play();
    }
    public void Resume()
    {
        pausePanel.SetActive(false);
        happy.SetActive(true);
        sad.SetActive(true);
        fear.SetActive(true);
        angry.SetActive(true);
        pause.SetActive(true);
        spawn.SetActive(true);
        rotate.SetActive(true);
        Time.timeScale = 1;
        UI.Play();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        UI.Play();
    }
    public void Options()
    {
        SceneManager.LoadScene("Option");
        UI.Play();
    }
}
