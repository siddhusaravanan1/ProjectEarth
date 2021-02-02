using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public AudioSource UI;

    public GameObject SoundOn;
    public GameObject SoundOff;
    public void NewGame()
    {
        SceneManager.LoadScene("SampleScene");
        UI.Play();
    }
    public void option()
    {
        SceneManager.LoadScene("Option");
        UI.Play();
    }
    public void BackOpt()
    {
        SceneManager.LoadScene("MainMenu");
        UI.Play();
    }
    public void Quit()
    {
        Application.Quit();
        UI.Play();
    }
    public void Next()
    {
        SceneManager.LoadScene("SampleScene");
        UI.Play();
    }
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
        UI.Play();
    }
    public void SoundOnSwitch()
    {
        SoundOff.SetActive(true);
        SoundOn.SetActive(false);
        Camera.main.GetComponent<AudioListener>().enabled = false;
    }
    public void SoundOffSwitch()
    {
        SoundOff.SetActive(false);
        SoundOn.SetActive(true);
        Camera.main.GetComponent<AudioListener>().enabled = true;
    }

}
