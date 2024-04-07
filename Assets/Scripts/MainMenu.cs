using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject canvas1;
    public GameObject canvas2;
    public GameObject gameObjectToDeactivate;

    public void Play1Level()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Play2Level()
    {
    SceneManager.LoadScene("SecondLevel");
    }

    public void ChooseLevel()
    {
        canvas2.gameObject.SetActive(true);
        gameObjectToDeactivate.SetActive(false);
        canvas1.gameObject.SetActive(false);
            }
}
